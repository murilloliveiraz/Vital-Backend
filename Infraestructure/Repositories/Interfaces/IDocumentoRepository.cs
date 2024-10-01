using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDocumentoRepository
    {
        Task<Documento?> Create(Documento model);
    }
}