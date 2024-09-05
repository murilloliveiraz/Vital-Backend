using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRegistroRepository
    {
        Task CreateRecord(ProntuarioRegistro registro);
        Task<ICollection<ProntuarioRegistro>> GetAllRecords(int prontuarioId);
    }
}
