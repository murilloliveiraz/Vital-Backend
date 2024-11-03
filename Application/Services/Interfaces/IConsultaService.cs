using Application.DTOS.Consulta;
using Application.DTOS.Exame;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IConsultaService : IService<AgendarConsultaRequestContract, AgendarConsultaResponseContract, int>
    {
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllScheduled();
        Task<AgendarConsultaResponseContract> CreateRemoteAppointment(AgendarConsultaRequestContract model);
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllCompleted();
        Task<ConsultaConcluidaResponse> SetAppointmentAsCompleted(int id);
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllPatientAppointmentsScheduled(int id);
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllPatientAppointmentsCompleted(int id);
        Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllDoctorAppointmentsScheduled(int id);
        Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllDoctorAppointmentsCompleted(int id);
    }
}