using Application.DTOS.Usuario;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Register(UsuarioRequestContract model);
        Task<UsuarioLoginResponseContract> Login(UsuarioLoginRequestContract model);
    }
}