using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProntuarioRepository
    {
        Task<Prontuario> Create(Prontuario prontuario);
    }
}
