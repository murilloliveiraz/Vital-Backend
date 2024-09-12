using Application.DTOS.Usuario;
using Application.DTOS.Utils;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Register(UsuarioRequestContract model);
        Task<UsuarioLoginResponseContract> Login(UsuarioLoginRequestContract model);
        Task<OperationResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<OperationResult> ForgotMyPassword(string email);
    }
}