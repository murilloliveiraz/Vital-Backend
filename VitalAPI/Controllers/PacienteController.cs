using Application.DTOS.Paciente;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PacienteRequestContract model)
        {
            var paciente = await _pacienteService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pacienteService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _pacienteService.Get());
        }

        [HttpGet("pesquisar/cpf")]
        public async Task<IActionResult> GetByCPF(string cpf)
        {
            return Ok(await _pacienteService.GetByCPF(cpf));
        }

        [HttpGet("pesquisar/email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _pacienteService.GetByEmail(email));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _pacienteService.GetById(id));
        }

        [HttpGet("pesquisar/nome")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _pacienteService.GetByName(name));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PacienteRequestContract model)
        {
            await _pacienteService.Update(id, model);
            return Ok();
        }
    }
}
