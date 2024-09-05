using Application.DTOS.Prontuarios;
using Application.DTOS.RegistroProntuario;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IProntuarioService
    {
        Task<Prontuario> Create(int pacienteId);
        Task CreateRecord(int prontuarioId, RegistroRequestContract registro);
        Task<ICollection<RegistroResponseContract>> GetAllRecords(int pacienteId);
    }
}
