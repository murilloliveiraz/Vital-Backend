using Application.DTOS.RegistroProntuario;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace VitalAPI.Controllers
{
    [ApiController]
    [Route("api/prontuarios/")]
    public class ProntuarioController : ControllerBase
    {
        private readonly IProntuarioService _prontuarioService;

        public ProntuarioController(IProntuarioService prontuarioService)
        {
            _prontuarioService = prontuarioService;
        }

        [HttpPost("{prontuarioId}/registros")]
        [Authorize(Policy = "AdminOrMedico")]
        public async Task<IActionResult> CreateRecord(int prontuarioId, [FromBody] RegistroRequestContract dto)
        {
            if (dto == null)
            {
                return BadRequest("O corpo da requisição não pode ser nulo.");
            }
            await _prontuarioService.CreateRecord(prontuarioId, dto);
            return Ok();
        }

        [HttpGet("{pacienteId}")]
        [Authorize(Policy = "AdminOrMedico")]
        public async Task<IActionResult> GetPatientMedialRecord(int pacienteId)
        {
            var prontuario = await _prontuarioService.GetAllRecords(pacienteId);
            if (prontuario == null)
                return NotFound();
            return Ok(prontuario);
        }

        [HttpGet("buscar-registro/{registroId}")]
        public async Task<IActionResult> GetRegistroById(string registroId)
        {
            if (!ObjectId.TryParse(registroId, out ObjectId objectId))
            {
                return BadRequest("Invalid ID format.");
            }

            var registro = await _prontuarioService.GetById(objectId);

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }
    }
}
