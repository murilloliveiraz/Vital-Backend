using Application.Helpers;
using Application.Services.Interfaces;
using Domain;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;
using System.Text.Json;

namespace Application.Services.Classes
{
    public class GoogleMeetService : IGoogleMeetService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;

        public GoogleMeetService(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
        }

        public async Task<object> CreateConsulta(string accessToken, Consulta consulta)
        {
            if (string.IsNullOrEmpty(accessToken))
                return new { error = "Missing or invalid authorization token." };

            string apiKey = _configuration["google:api_key"];
            string requestUri = $"https://www.googleapis.com/calendar/v3/calendars/primary/events";
            var guid = Guid.NewGuid().ToString("N");

            var googleMeetEvent = new GoogleMeetEvent();
            googleMeetEvent.ConferenceData = BuildConferenceData(guid);
            googleMeetEvent.Summary = consulta.Nome;
            googleMeetEvent.Description = consulta.Local;
            googleMeetEvent.Start.DateTime = consulta.Data;
            googleMeetEvent.End.DateTime = consulta.Data.AddMinutes(20);

            var createResponse = await ExecutePostRequest(requestUri, apiKey, accessToken, googleMeetEvent);

            if (createResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var createdEvent = JsonSerializer.Deserialize<JObject>(createResponse.Content);
                var eventId = createdEvent["id"]?.ToString();
                if (string.IsNullOrEmpty(eventId))
                    return "Failed to retrieve event ID.";

                return await GetConsulta(accessToken, eventId);
            }

            return new { StatusCode = (int)createResponse.StatusCode, error =  createResponse.ErrorMessage};
        }
        private async Task<RestResponse> ExecutePostRequest(string uri, string apiKey, string accessToken, GoogleMeetEvent model)
        {
            var client = new RestClient(uri);
            var request = new RestRequest();
            request.AddQueryParameter("key", apiKey);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(model);

            return await client.PostAsync(request);
        }

        private ConferenceData BuildConferenceData(string requestId)
        {
            return new ConferenceData()
            {
                CreateRequest = new CreateConferenceRequest()
                {
                    RequestId = requestId,
                    ConferenceSolutionKey = new ConferenceSolutionKey()
                    {
                        Type = "hangoutsMeet"
                    }
                }
            };
        }

        public async Task<string> ExchangeCodeForToken(string authorizationCode)
        {
            if (string.IsNullOrEmpty(authorizationCode))
                return "Authorization code cannot be null or empty";

            var body = new StringContent(
               $"code={authorizationCode}&client_id={_configuration["google:client_id"]}&client_secret={_configuration["google:client_secret"]}&redirect_uri={_configuration["google:redirect_uri"]}&grant_type=authorization_code",
               Encoding.UTF8,
               "application/x-www-form-urlencoded");

            try
            {
                var _httpClient = _factory.CreateClient();
                var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", body);
                
                if (!response.IsSuccessStatusCode)
                {
                    return $"Error exchanging code for token: {response.ReasonPhrase}";
                }

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                return $"Error exchanging code for token: {ex.Message}";
            }
        }

        public async Task<object> GetConsulta(string accessToken, string id)
        {
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(id))
                return new { error = "Invalid parameters" };

            string requestUri = $"https://www.googleapis.com/calendar/v3/calendars/primary/events/{id}?conferenceDataVersion=1&key={_configuration["google:api_key"]}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");

            var client = _factory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventDetails = JsonSerializer.Deserialize<JsonDocument>(content);

                var hangoutLink = eventDetails?.RootElement.GetProperty("hangoutLink").GetString();
                if (!string.IsNullOrEmpty(hangoutLink))
                {
                    return new { meetLink = hangoutLink };
                }
                else
                {
                    return "Failed to retrieve Hangouts Meet link";
                }
            }
            else
            {
                return new { status = (int)response.StatusCode, error = response.ReasonPhrase };
            }
        }

        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return "Refresh token cannot be null or empty";

            var body = new StringContent(
               $"client_id={_configuration["google:client_id"]}&client_secret={_configuration["google:client_secret"]}&refresh_token={refreshToken}&grant_type=refresh_token",
               Encoding.UTF8,
               "application/x-www-form-urlencoded");

            try
            {
                var _httpClient = _factory.CreateClient();
                var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", body);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error refreshing token: {response.ReasonPhrase}";
                }

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                return $"Error refreshing token: {ex.Message}";
            }
        }
    }
}
