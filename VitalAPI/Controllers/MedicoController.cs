using Application.DTOS.Medico;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [ApiController]
    [Route("api/medicos")]
    public class MedicoController : ControllerBase
    {
         private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicoRequestContract model)
        {
            var medico = await _medicoService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _medicoService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _medicoService.Get());
        }

        [HttpGet("pesquisar/crm")]
        public async Task<IActionResult> GetByCRM(string crm)
        {
            return Ok(await _medicoService.GetByCRM(crm));
        }

        [HttpGet("pesquisar/especializacao")]
        public async Task<IActionResult> GetAllBySpecialization(string specialization)
        {
            return Ok(await _medicoService.GetAllBySpecialization(specialization));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _medicoService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicoRequestContract model)
        {
            await _medicoService.Update(id, model);
            return Ok();
        }
    }
}