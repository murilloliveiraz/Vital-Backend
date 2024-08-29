namespace Application.DTOS.Usuario
{
    public class UsuarioLoginRequestContract
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
