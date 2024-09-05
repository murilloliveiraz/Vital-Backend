namespace Application.DTOS.Admin
{
    public class AdminResponseContract : AdminRequestContract
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Cargo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CriadoPorUsuarioId { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
    }
}