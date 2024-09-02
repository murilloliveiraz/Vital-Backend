using Application.DTOS.Paciente;

namespace Application.Services.Interfaces
{
    public interface IPacienteService : IService<PacienteRequestContract, PacienteResponseContract, int>
    {
        Task<PacienteResponseContract?> GetByCPF(string cpf);
        Task<PacienteResponseContract?> GetByName(string name);
        Task<PacienteResponseContract?> GetByEmail(string email);
    }
}