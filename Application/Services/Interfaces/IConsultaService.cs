using Application.DTOS.Consulta;
using Application.DTOS.Exame;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IConsultaService : IService<AgendarConsultaRequestContract, AgendarConsultaResponseContract, int>
    {
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllScheduled();
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllCompleted();
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllPatientAppointmentsScheduled(int id);
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllPatientAppointmentsCompleted(int id);
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllDoctorAppointmentsScheduled(int id);
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllDoctorAppointmentsCompleted(int id);
    }
}