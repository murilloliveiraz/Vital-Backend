using Application.DTOS.Usuario;

namespace Application.DTOS.Admin
{
    public class AdminRequestContract : UsuarioRequestContract
    {
        public string Cargo { get; set; }
    }
}