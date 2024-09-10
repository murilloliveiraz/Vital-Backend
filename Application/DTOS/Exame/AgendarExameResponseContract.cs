namespace Application.DTOS.Exame
{
    public class AgendarExameResponseContract : AgendarExameRequestContract
    {
        public int ExameId { get; set; }
        public string EmailParaReceberResultado { get; set; }
    }
}
