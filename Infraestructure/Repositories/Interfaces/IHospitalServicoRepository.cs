using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IHospitalServicoRepository
    {
        Task<IEnumerable<HospitalServico?>> GetAllByHospitalId(int hospitalId);
        Task<HospitalServico> GetByHospitalIdAndServicoId(int hospitalId, int servicoId);
        Task<HospitalServico> Create(HospitalServico model);
        Task<IEnumerable<HospitalServico?>> GetAllHospitalsThatOfferAnSpecificService(int servicoId);
        Task Delete(HospitalServico model);
    }
}