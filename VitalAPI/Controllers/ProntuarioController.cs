using Application.DTOS.RegistroProntuario;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/prontuarios/")]
    [ApiController]
    public class ProntuarioController : ControllerBase
    {
        private readonly IProntuarioService _prontuarioService;

        public ProntuarioController(IProntuarioService prontuarioService)
        {
            _prontuarioService = prontuarioService;
        }

        [HttpPost("{prontuarioId}/registros")]
        public async Task<IActionResult> CreateRecord(int prontuarioId, [FromBody] RegistroRequestContract dto)
        {
            await _prontuarioService.CreateRecord(prontuarioId, dto);
            return Ok();
        }

        [HttpGet("{pacienteId}")]
        public async Task<IActionResult> GetPatientMedialRecord(int pacienteId)
        {
            var prontuario = await _prontuarioService.GetAllRecords(pacienteId);
            if (prontuario == null)
                return NotFound();
            return Ok(prontuario);
        }
    }
}
