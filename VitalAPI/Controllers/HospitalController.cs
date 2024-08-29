using Application.DTOS.Hospital;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/hospitais")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _hospitalService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _hospitalService.GetById(id));
        }
        
        [HttpGet("pesquisar/nome")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _hospitalService.GetByName(name));
        }
        
        [HttpGet("pesquisar/estado")]
        public async Task<IActionResult> GetAllByLocation(string estado)
        {
            return Ok(await _hospitalService.GetAllByLocation(estado));
        }

        [HttpPost]
        public async Task<IActionResult> Post(HospitalRequestContract hospital)
        {
            var createdHospital = await _hospitalService.Create(hospital);
            return CreatedAtAction(nameof(GetById), new { id = createdHospital.Id }, createdHospital);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hospitalService.Delete(id);
            return NoContent();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HospitalRequestContract hospital)
        {
            await _hospitalService.Update(id, hospital);
            return Ok();
        }
    }
}