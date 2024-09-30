namespace Domain
{
    public class CreditCardPayment
    {
        public decimal TransactionAmount { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public int Installments { get; set; }
        public string PaymentMethodId { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
