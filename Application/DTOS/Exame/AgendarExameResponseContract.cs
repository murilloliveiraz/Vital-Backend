namespace Application.DTOS.Exame
{
    public class AgendarExameResponseContract : AgendarExameRequestContract
    {
        public int Id { get; set; }
        public string EmailParaReceberResultado { get; set; }
        public string Status { get; set; }
        public string PacienteNome { get; set; }
        public string? StatusPagamento { get; set; }
    }
}
