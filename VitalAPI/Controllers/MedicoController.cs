using Application.DTOS.Medico;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(MedicoRequestContract model)
        {
            var medico = await _medicoService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _medicoService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _medicoService.Get());
        }

        [HttpGet("pesquisar/crm")]
        [Authorize]
        public async Task<IActionResult> GetByCRM(string crm)
        {
            return Ok(await _medicoService.GetByCRM(crm));
        }

        [HttpGet("pesquisar/email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _medicoService.GetByEmail(email));
        }

        [HttpGet("pesquisar/especializacao")]
        [Authorize]
        public async Task<IActionResult> GetAllBySpecialization(string specialization)
        {
            return Ok(await _medicoService.GetAllBySpecialization(specialization));
        }

        [HttpGet("pesquisar/especializacao-e-hospital")]
        [Authorize]
        public async Task<IActionResult> GetAllBySpecializationAndHospitalId(string specialization, int id)
        {
            return Ok(await _medicoService.GetAllBySpecializationAndHospitalId(specialization, id));
        }
        
        [HttpGet("pesquisar/hospital")]
        [Authorize]
        public async Task<IActionResult> GetAllByHospitalId(int id)
        {
            return Ok(await _medicoService.GetAllByHospitalId(id));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _medicoService.GetById(id));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, MedicoRequestContract model)
        {
            await _medicoService.Update(id, model);
            return Ok();
        }
    }
}