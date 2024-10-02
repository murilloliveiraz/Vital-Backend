using Application.DTOS.Consulta;

namespace Application.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<AdicionarDocumentoResponseContract> AttachDocument(AdicionarDocumentoRequestContract model);
    }
}