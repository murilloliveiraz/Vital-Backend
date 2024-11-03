namespace Domain
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public decimal? ValorConsulta { get; set; }
        public string? StatusPagamento { get; set; }
        public string TipoConsulta { get; set; }
        public string? Local { get; set; }
        public string? MeetLink { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public string PrefixoDaPasta { get; set; }
        public string EmailParaReceberNotificacoes { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}
