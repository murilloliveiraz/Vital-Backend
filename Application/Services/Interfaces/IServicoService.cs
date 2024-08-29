using Application.DTOS.Servicos;

namespace Application.Services.Interfaces
{
     public interface IServicoService : IService<ServicoRequestContract, ServicoResponseContract, int>
    {
        Task<ServicoResponseContract> GetByName(string name);
        Task<IEnumerable<ServicoResponseContract>> GetAllIncludingDeleteds();
    }
}