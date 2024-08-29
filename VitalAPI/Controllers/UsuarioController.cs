using Application.DTOS.Usuario;
using Application.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _userService;

        public UsuarioController(UsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(UsuarioRequestContract model)
        {
            return Created("", await _userService.Register(model));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(UsuarioLoginRequestContract model)
        {
            return Ok(await _userService.Login(model));
        }
    }
}
