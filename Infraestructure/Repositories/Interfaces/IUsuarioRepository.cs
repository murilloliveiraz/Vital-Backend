using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, int>
    {
        Task<Usuario?> GetByName(string name);
        Task<Usuario?> GetByEmail(string email);
        Task<Usuario?> UpdatePassword(string password);
    }
}