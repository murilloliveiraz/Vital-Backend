namespace Domain
{
    public class CreditCardPayment
    {
        public decimal TransactionAmount { get; set; }
        public string Token { get; set; }
        public string NomeServico { get; set; }
        public decimal ValorConsulta { get; set; }
        public int Installments { get; set; }
        public int ConsultaId { get; set; }
        public string NomePagador { get; set; }
        public string SobrenomePagador { get; set; }
        public string PaymentMethodId { get; set; }
        public string EmailPagador { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
