namespace Application.DTOS.Exame
{
    public class ExameConcluidoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Local { get; set; }
        public int MedicoId { get; set; }
        public DateTime Data { get; set; }
        public string S3KeyPath { get; set; }
        public string ArquivoResultadoUrl { get; set; }
        public string? StatusPagamento { get; set; }
        public string? UrlResultadoClinicaExterna { get; set; }
    }
}