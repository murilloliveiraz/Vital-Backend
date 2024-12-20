using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMedicoRepository: IRepository<Medico, int>
    {
        Task<Medico?> GetByCRM(string crm);
        Task<Medico?> GetByEmail(string email);
        Task<IEnumerable<Medico>?> GetAllBySpecialization(string especialization);
        Task<IEnumerable<Medico>?> GetAllBySpecializationAndHospitalId(string especialization, int id);
        Task<IEnumerable<Medico>?> GetAllByHospitalId(int id);
    }
}