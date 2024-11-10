using Domain;

namespace Application.DTOS.Consulta
{
    public class ConsultaConcluidaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal? ValorConsulta { get; set; }
        public string? Local { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string TipoConsulta { get; set; }
        public string Status { get; set; }
        public ICollection<AdicionarDocumentoResponseContract> Documentos { get; set; }
        
    }
}