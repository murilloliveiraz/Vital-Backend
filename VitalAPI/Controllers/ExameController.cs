using Application.DTOS.Exame;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/exames")]
    [ApiController]
    public class ExameController : ControllerBase
    {
        private readonly IExameService _exameService;

        public ExameController(IExameService exameService)
        {
            _exameService = exameService;
        }

        [HttpPost("agendar")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(AgendarExameRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exame = await _exameService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = exame.Id }, exame);
        }

        [HttpPost("{id}/anexar-resultado")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AttachResult(int id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new AdicionarResultadoRequestContract
            {
                ExameId = id,
                File = file
            };
            var result = await _exameService.AttachResult(model);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get()
        {
            var exames = await _exameService.Get();
            return Ok(exames);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var exame = await _exameService.GetById(id);
            if (exame == null)
            {
                return NotFound();
            }

            return Ok(exame);
        }

        [HttpGet("concluidos")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllCompleted()
        {
            var examesConcluidos = await _exameService.GetAllCompleted();
            return Ok(examesConcluidos);
        }

        [HttpGet("paciente/{id}/concluidos")]
        [Authorize]
        public async Task<IActionResult> GetAllPatientExamsCompleted(int id)
        {
            var examesConcluidos = await _exameService.GetAllPatientExamsCompleted(id);
            return Ok(examesConcluidos);
        }

        [HttpGet("paciente/{id}/agendados")]
        [Authorize]
        public async Task<IActionResult> GetAllPatientExamsScheduled(int id)
        {
            var examesAgendados = await _exameService.GetAllPatientExamsScheduled(id);
            return Ok(examesAgendados);
        }

        [HttpGet("medico/{id}/concluidos")]
        [Authorize]
        public async Task<IActionResult> GetAllDoctorExamsCompleted(int id)
        {
            var examesConcluidos = await _exameService.GetAllDoctorExamsCompleted(id);
            return Ok(examesConcluidos);
        }

        [HttpGet("medico/{id}/agendados")]
        [Authorize]
        public async Task<IActionResult> GetAllDoctorExamsScheduled(int id)
        {
            var examesAgendados = await _exameService.GetAllDoctorExamsScheduled(id);
            return Ok(examesAgendados);
        }

        [HttpGet("agendados")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllScheduled()
        {
            var examesAgendados = await _exameService.GetAllScheduled();
            return Ok(examesAgendados);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, [FromBody] AgendarExameRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _exameService.Update(id, model);
            return Ok(result);
        }

        [HttpPut("concluir/{id}")]
        [Authorize(Policy = "AdminOrMedico")]
        public async Task<IActionResult> SetExamAsCompleted(int id)
        {
            var result = await _exameService.SetExamAsCompleted(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exameService.Delete(id);
            return NoContent();
        }
    }
}
