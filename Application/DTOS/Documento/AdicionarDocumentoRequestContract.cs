using Microsoft.AspNetCore.Http;

namespace Application.DTOS.Consulta
{
    public class AdicionarDocumentoRequestContract
    {
        public int ConsultaId { get; set; }
        public IFormFile File;
    }
}