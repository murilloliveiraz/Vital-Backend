using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository<Administrador, int>
    {
        Task<Administrador?> GetByEmail(string email);
    }
}