using Application.DTOS.Hospital;
using Application.DTOS.HospitalServico;
using Application.DTOS.Servicos;

namespace Application.Services.Interfaces
{
    public interface IHospitalServicoService
    {
        Task<HospitalServicoResponseContract> Create(HospitalServicoRequestContract model);
        Task Delete(int hospitalId, int servicoId);
        Task<IEnumerable<ServicoResponseContract?>> GetAllByHospitalId(int hospitalId);
        Task<IEnumerable<HospitalResponseContract?>> GetAllHospitalsThatOfferAnSpecificService(int servicoId);
        Task<ServicoResponseContract?> GetByHospitalIdAndServicoId(int hospitalId, int servicoId);
    }
}