namespace Domain
{
    public class PixPayment
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sobrenome { get; set; }
        public string Servico { get; set; }
        public string NotificationURL { get; set; }
        public decimal Valor { get; set; }
    }
}
