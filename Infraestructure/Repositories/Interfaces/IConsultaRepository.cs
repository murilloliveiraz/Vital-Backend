using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta, int>
    {
        Task<IEnumerable<Consulta>?> GetAllScheduled();
        Task<IEnumerable<Consulta>?> GetAllCompleted();
        Task<Consulta> GetByPaymentId(int id);
        Task<Consulta> SetAppointmentAsCompleted(int id);
        Task<Consulta> SetPaymentId(int id, long paymentId);
        Task<Consulta> UpdatePaymentStatus(long id, string status);
        Task<IEnumerable<Consulta>?> GetAllPatientAppointmentsScheduled(int id);
        Task<IEnumerable<Consulta>?> GetAllPatientAppointmentsCompleted(int id);
        Task<IEnumerable<Consulta>?> GetAllDoctorAppointmentsScheduled(int id);
        Task<IEnumerable<Consulta>?> GetAllDoctorAppointmentsCompleted(int id);
    }
}
