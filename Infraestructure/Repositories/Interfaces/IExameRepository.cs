using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IExameRepository : IRepository<Exame, int>
    {
        Task<IEnumerable<Exame>?> GetAllScheduled();

        Task<IEnumerable<Exame>?> GetAllCompleted();
        Task<IEnumerable<Exame>?> GetAllPatientExamsScheduled(int id);

        Task<IEnumerable<Exame>?> GetAllPatientExamsCompleted(int id);
    }
}
