using Application.Helpers;
using Application.Services.Interfaces;
using Domain;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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

        public GoogleMeetService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        const string CALENDAR_ID = "primary";

        private async Task<CalendarService> ConnectGoogleCalendar(string[] scopes)
        {
            string applicationName = "Vital";
            UserCredential credential;
            var directory = _configuration["ApplicationLocation:Location"];

            using (var stream = new FileStream(Path.Combine(directory, "Helpers", "calendarcredential.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = $"{directory}\\CalendarAuthToken";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                );
            }

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return services;
        }

        public async Task<Event> CreateEvent(Consulta request, string pacienteEmail, string medicoEmail)
        {
            string[] scopes = { "https://www.googleapis.com/auth/calendar " };
            var services = await ConnectGoogleCalendar(scopes);

            Event eventCalendar = new Event()
            {
                Summary = request.Nome,
                Location = request.TipoConsulta,
                Start = new EventDateTime
                {
                    DateTimeDateTimeOffset = request.Data,
                },
                End = new EventDateTime
                {
                    DateTimeDateTimeOffset = request.Data.AddMinutes(20),
                },
                Attendees = new List<EventAttendee>
                {
                    new EventAttendee { Email = pacienteEmail },
                    new EventAttendee { Email = medicoEmail },
                },
                Description = "Consulta médica com profissional da saúde da VITAL. Este evento está agendado para garantir o atendimento pontual e de qualidade que você merece. Por favor, verifique os detalhes e esteja preparado para fornecer informações relevantes para o seu acompanhamento médico. Em caso de imprevistos, entre em contato com antecedência para remarcar ou cancelar o compromisso. A VITAL está comprometida com o seu bem-estar e saúde!",
                ConferenceData = new ConferenceData()
                {
                    CreateRequest = new CreateConferenceRequest()
                    {
                        ConferenceSolutionKey = new ConferenceSolutionKey()
                        {
                            Type = "hangoutsMeet"
                        },
                        RequestId = Guid.NewGuid().ToString()
                    },
                },
            };

            var eventRequest = services.Events.Insert(eventCalendar, CALENDAR_ID);
            eventRequest.ConferenceDataVersion = 1;
            var createdEvent = await eventRequest.ExecuteAsync();

            return createdEvent;
        }
    }
}
