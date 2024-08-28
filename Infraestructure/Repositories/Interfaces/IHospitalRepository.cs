using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IHospitalRepository : IRepository<Hospital, int>
    {
        Task<Hospital?> GetByName(string name);
        Task<Hospital?> GetAllByLocation(string name);
    }
}