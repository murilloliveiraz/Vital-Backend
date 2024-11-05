using Application.Services.Interfaces;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/cartao-credito")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;
        private readonly IConsultaRepository _consultaRepository;

        public CreditoController(IConsultaRepository consultaRepository, ICreditCardService creditCardService)
        {
            _consultaRepository = consultaRepository;
            _creditCardService = creditCardService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreditCardPayment model)
        {
            var payment = await _creditCardService.CobrarComCartaoDeCredito(model);
            await _consultaRepository.UpdatePaymentStatus(model.ConsultaId, "Pendente");
            if (payment.Id.HasValue)
            {
                await _consultaRepository.SetPaymentId(model.ConsultaId, payment.Id.Value);
            }
            return Ok(payment);
        }
    }
}
