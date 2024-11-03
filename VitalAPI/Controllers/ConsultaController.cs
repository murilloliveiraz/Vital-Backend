using Application.DTOS.Consulta;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/consultas")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpPost("agendar-remota")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(AgendarConsultaRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consulta = await _consultaService.CreateRemoteAppointment(model);
            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var consulta = await _consultaService.GetById(id);
            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }
    }
}
