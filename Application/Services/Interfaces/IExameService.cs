using Application.DTOS.Medico;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IExameService: IService<MedicoRequestContract, MedicoResponseContract, int>
    {
        Task<IEnumerable<Exame>?> GetAllScheduled();

        Task<IEnumerable<Exame>?> GetAllCompleted();
    }
}
