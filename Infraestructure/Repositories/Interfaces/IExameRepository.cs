using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IExameRepository : IRepository<Exame, int>
    {
        Task<IEnumerable<Exame>?> GetAllScheduled();

        Task<IEnumerable<Exame>?> GetAllCompleted();
    }
}
