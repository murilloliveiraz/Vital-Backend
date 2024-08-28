using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPacienteRepository : IUsuarioRepository
    {
        Task<Paciente?> GetByCPF(string name);
    }
}