using Application.DTOS.Medico;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IMedicoService : IService<MedicoRequestContract, MedicoResponseContract, int>
    {
        Task<MedicoResponseContract?> GetByCRM(string crm);
        Task<MedicoResponseContract?> GetByEmail(string email);
        Task<IEnumerable<MedicoResponseContract>?> GetAllBySpecialization(string especialization);
        Task<IEnumerable<MedicoResponseContract>?> GetAllBySpecializationAndHospitalId(string especialization, int id);
        Task<IEnumerable<MedicoResponseContract>?> GetAllByHospitalId(int id);
    }
}