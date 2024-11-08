namespace Application.DTOS.Consulta
{
    public class AgendarConsultaResponseContract
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorConsulta { get; set; }
        public string TipoConsulta { get; set; }
        public string? Local { get; set; }
        public DateTime Data { get; set; }
        public string QueixasDoPaciente { get; set; }
        public string PacienteNome { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string EmailParaReceberNotificacoes { get; set; }
        public string Status { get; set; }
        public string? StatusPagamento { get; set; }
        public string? MeetLink { get; set; }
    }
}