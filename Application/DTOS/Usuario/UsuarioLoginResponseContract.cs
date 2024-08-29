namespace Application.DTOS.Usuario
{
    public class UsuarioLoginResponseContract
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
