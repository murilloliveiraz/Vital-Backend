namespace Domain
{
    public class Documento
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public string S3KeyPath { get; set; }
        public Consulta Consulta { get; set; }
    }
}