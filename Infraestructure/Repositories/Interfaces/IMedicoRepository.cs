using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMedicoRepository: IUsuarioRepository
    {
        Task<Medico?> GetByCRM(string name);
        Task<IEnumerable<Medico>?> GetAllBySpecialization(string name);
    }
}