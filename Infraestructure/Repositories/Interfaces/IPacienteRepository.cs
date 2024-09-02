using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente, int>
    {
        Task<Paciente?> GetByCPF(string cpf);
        Task<Paciente?> GetByName(string name);
        Task<Paciente?> GetByEmail(string email);
    }
}