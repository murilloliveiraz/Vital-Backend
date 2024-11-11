using Application.DTOS.Prontuarios;
using Application.DTOS.RegistroProntuario;
using Domain;
using MongoDB.Bson;

namespace Application.Services.Interfaces
{
    public interface IProntuarioService
    {
        Task<Prontuario> Create(int pacienteId);
        Task CreateRecord(int prontuarioId, RegistroRequestContract registro);
        Task<ICollection<RegistroResponseContract>> GetAllRecords(int pacienteId);
        Task<RegistroResponseContract> GetById(ObjectId registroId);
    }
}
