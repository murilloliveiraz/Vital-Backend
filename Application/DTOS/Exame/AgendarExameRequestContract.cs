namespace Application.DTOS.Exame
{
    public class AgendarExameRequestContract
    {
        public string Nome { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string EmailParaReceberResultado { get; set; }
    }
}
