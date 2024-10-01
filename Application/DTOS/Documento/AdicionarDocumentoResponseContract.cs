namespace Application.DTOS.Consulta
{
    public class AdicionarDocumentoResponseContract
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public string S3KeyPath { get; set; }
        public string ArquivoResultadoUrl { get; set; }
    }
}