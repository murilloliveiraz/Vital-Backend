using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IExameRepository : IRepository<Exame, int>
    {
        Task<IEnumerable<Exame>?> GetAllScheduled();
        Task<IEnumerable<Exame>?> GetAllCompleted();
        Task<Exame> SetExamAsCompleted(int id);
        Task<Exame> AddExternURL(int id, string url);
        Task<Exame> UpdatePaymentStatus(int id);
        Task<IEnumerable<Exame>?> GetAllPatientExamsScheduled(int id);
        Task<IEnumerable<Exame>?> GetAllPatientExamsCompleted(int id);
        Task<IEnumerable<Exame>?> GetAllDoctorAppointmentsScheduled(int id);
        Task<IEnumerable<Exame>?> GetAllDoctorAppointmentsCompleted(int id);
    }
}
