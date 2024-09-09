using Microsoft.AspNetCore.Http;

namespace Application.DTOS.Exame
{
    public class AdicionarResultadoRequestContract
    {
        public int ExameId { get; set; }
        public IFormFile File;
        public string Prefix;
    }
}
