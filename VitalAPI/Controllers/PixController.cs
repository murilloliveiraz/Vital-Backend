using Application.DTOS.Paciente;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/pix")]
    [ApiController]
    public class PixController : ControllerBase
    {
        private readonly IPixService _pixService;
        private readonly IConsultaRepository _consultaRepository;

        public PixController(IPixService pixService, IConsultaRepository consultaRepository)
        {
            _pixService = pixService;
            _consultaRepository = consultaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PixPayment model)
        {
            var payment = await _pixService.CobrarComPix(model);
            await _consultaRepository.UpdatePaymentStatus(model.ConsultaId, "Pendente");
            if (payment.Id.HasValue)
            {
                await _consultaRepository.SetPaymentId(model.ConsultaId, payment.Id.Value);
            }
            return Ok(payment);
        }
    }
}
