namespace Domain
{
    public class Exame
    {
        public int ExameId { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public string CaminhoResultado { get; set; }
        public string ArquivoResultadoUrl { get; set; }
        public string EmailParaReceberResultado { get; set; }
    }
}
