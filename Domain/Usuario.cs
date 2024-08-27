using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public int CriadoPorUsuarioId { get; set; }
    }
}
