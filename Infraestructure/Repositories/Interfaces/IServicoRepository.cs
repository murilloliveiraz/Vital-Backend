using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IServicoRepository : IRepository<Servico, int>
    {
        Task<Servico?> GetByName(string name); 
        Task<IEnumerable<Servico?>> GetAllIncludingDeleteds();
        Task<IEnumerable<Servico?>> GetAllExamsServices();
        Task<IEnumerable<Servico?>> GetAllAppointmentsServices();
    }

}