using Application.DTOS.HospitalServico;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [ApiController]
    [Route("api/hospital-servicos")]
    public class HospitalServicoController : ControllerBase
    {
        private readonly IHospitalServicoService _hospitalServicoService;
        public HospitalServicoController(IHospitalServicoService hospitalServicoService)
        {
            _hospitalServicoService = hospitalServicoService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByHospitalId (int id)
        {
            return Ok(await _hospitalServicoService.GetAllByHospitalId(id));
        }

        [HttpGet("{hospitalId}/{servicoId}")]
        [Authorize]
        public async Task<IActionResult> GetByHospitalIdAndServicoId(int hospitalId, int servicoId)
        {
            return Ok(await _hospitalServicoService.GetByHospitalIdAndServicoId(hospitalId, servicoId));
        } 

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post(HospitalServicoRequestContract hospitalServico)
        {
            var createdHospitalServico = await _hospitalServicoService.Create(hospitalServico);
            return CreatedAtAction(
                nameof(GetByHospitalIdAndServicoId),
                new { hospitalId = hospitalServico.HospitalId, servicoId = hospitalServico.ServicoId }, createdHospitalServico
            );
        }

        [HttpDelete("{hospitalId}/{servicoId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int hospitalId, int servicoId)
        {
            await _hospitalServicoService.Delete(hospitalId, servicoId);
            return NoContent();
        } 
    }
}