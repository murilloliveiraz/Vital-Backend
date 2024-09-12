using Application.DTOS.Servicos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/servicos")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _servicoService;
        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _servicoService.Get());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _servicoService.GetById(id));
        }
        
        [HttpGet("pesquisar/nome")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _servicoService.GetByName(name));
        }
        
        [HttpGet("listar-todos")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllIncludingDeleteds()
        {
            return Ok(await _servicoService.GetAllIncludingDeleteds());
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post(ServicoRequestContract servico)
        {
            var createdServico = await _servicoService.Create(servico);
            return CreatedAtAction(nameof(GetById), new { id = createdServico.ServicoId }, createdServico);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicoService.Delete(id);
            return NoContent();
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, ServicoRequestContract servico)
        {
            await _servicoService.Update(id, servico);
            return Ok();
        }
    }
}