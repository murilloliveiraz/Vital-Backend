﻿using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VitalAPI.Controllers
{
    [Route("api/notificacoes")]
    [ApiController]
    public class PaymentNotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(object request)
        {
            var jsonString = request.ToString();
            Console.WriteLine(request);
            var deserializedRequest = JsonSerializer.Deserialize<MercadoPagoResponseDTO>(jsonString);
            if (deserializedRequest != null)
            {
                string dataId = deserializedRequest.data?.id;
                string status = deserializedRequest.action;

                Console.WriteLine($"Data ID: {dataId}");
                Console.WriteLine($"Status: {status}");
                Console.WriteLine(deserializedRequest);
            }

            return Ok(request);
        }
    }
}
