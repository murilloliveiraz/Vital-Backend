using Application.DTOS.Consulta;
using Application.DTOS.Exame;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/documentos")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        public IDocumentoService _documentoService{ get; set; }

        public DocumentoController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }

        [HttpPost("{id}/anexar-arquivo")]
        [Authorize]
        public async Task<IActionResult> AttachResult(int id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new AdicionarDocumentoRequestContract
            {
                ConsultaId = id,
                File = file
            };
            var result = await _documentoService.AttachDocument(model);
            return Ok(result);
        }
    }
}
