using Application.DTOS.Usuario;
using Application.DTOS.Utils;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UsuarioController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(UsuarioRequestContract model)
        {
            return Created("", await _userService.Register(model));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(UsuarioLoginRequestContract model)
        {
            return Ok(await _userService.Login(model));
        }

        [HttpPost("loginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody]string credentials)
        {
            return Ok(await _userService.LoginWithGoogle(credentials));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("O e-mail é obrigatório.");
            }

            var result = await _userService.ForgotMyPassword(request.Email);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
