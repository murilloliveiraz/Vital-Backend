namespace Domain
{
    public class PixPayment
    {
        public string EmailPagador { get; set; }
        public string NomePagador { get; set; }
        public string CPFPagador { get; set; }
        public string SobrenomePagador { get; set; }
        public string NomeServico { get; set; }
        public int ConsultaId { get; set; }
        public decimal ValorConsulta { get; set; }
    }
}
