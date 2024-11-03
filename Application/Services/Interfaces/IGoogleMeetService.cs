using Domain;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IGoogleMeetService
    {
        Task<Event> CreateEvent(Consulta request, string pacienteEmail, string medicoEmail);
    }
}
