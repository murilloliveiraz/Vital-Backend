using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMedicoRepository: IRepository<Medico, int>
    {
        Task<Medico?> GetByCRM(string name);
        Task<IEnumerable<Medico>?> GetAllBySpecialization(string name);
    }
}