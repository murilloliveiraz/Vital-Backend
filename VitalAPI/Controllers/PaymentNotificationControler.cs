using Application.Helpers;
using Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VitalAPI.Controllers
{
    [Route("api/notificacoes")]
    [ApiController]
    public class PaymentNotificationController : ControllerBase
    {
        private readonly IConsultaRepository _consultaRepository;

        public PaymentNotificationController(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(object request)
        {
            var jsonString = request.ToString();
            var deserializedRequest = JsonSerializer.Deserialize<MercadoPagoResponseDTO>(jsonString);
            if (deserializedRequest != null)
            {
                long dataId = deserializedRequest.id;
                string status = deserializedRequest.action;

                var consulta = _consultaRepository.GetByPaymentId(dataId);
                if (consulta != null)
                {
                    _consultaRepository.UpdatePaymentStatus(consulta.Id);
                }

                Console.WriteLine($"Data ID: {dataId}");
                Console.WriteLine($"Status: {status}");
                Console.WriteLine(deserializedRequest);
            }

            return Ok(request);
        }
    }
}
