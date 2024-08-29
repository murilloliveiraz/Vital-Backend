namespace Application.DTOS.Servicos
{
    public class ServicoResponseContract : ServicoRequestContract
    {
        public int ServicoId { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}