namespace Application.DTOS.Consulta
{
    public class AgendarConsultaRequestContract
    {
        public string Nome { get; set; }
        public decimal ValorConsulta { get; set; }
        public string? Local { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string EmailParaReceberNotificacoes { get; set; }
    }
}