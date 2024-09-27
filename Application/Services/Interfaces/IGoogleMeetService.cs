using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IGoogleMeetService
    {
        Task<object> CreateConsulta(string access_Token, Consulta consulta);
        Task<object> GetConsulta(string access_Token, string id);
        Task<string> RefreshAccessToken(string refreshToken);
        Task<string> ExchangeCodeForToken(string authorizationCode);
    }
}
