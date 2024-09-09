namespace Application.DTOS.Exame
{
    public class AdicionarResultadoResponseContract
    {
        public int ExameId { get; set; }
        public string S3KeyPath { get; set; }
        public string ArquivoResultadoUrl { get; set; }
    }
}
