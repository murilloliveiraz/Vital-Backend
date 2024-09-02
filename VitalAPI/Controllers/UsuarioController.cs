using Application.DTOS.Usuario;
using Application.Services.Interfaces;
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
