namespace Application.DTOS.Exame
{
    public class AdicionarResultadoResponseContract
    {
        public int Id { get; set; }
        public string S3KeyPath { get; set; }
        public string ArquivoResultadoUrl { get; set; }
    }
}
