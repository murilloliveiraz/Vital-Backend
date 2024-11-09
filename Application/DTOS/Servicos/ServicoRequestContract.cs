namespace Application.DTOS.Servicos
{
   public class ServicoRequestContract
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Especializacao { get; set; }
        public string TipoServico { get; set; }
        public decimal Valor { get; set; }
    }
}