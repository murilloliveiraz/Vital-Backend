using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Classes
{
    public class PixService : IPixService
    {
        private IConfiguration _configuration;

        public PixService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Payment> CobrarComPix(PixPayment paymentRequest)
        {
            var requestOptions = new RequestOptions();
            string idempotencyKey = Guid.NewGuid().ToString();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idempotencyKey);
            var request = new PaymentCreateRequest
            {
                TransactionAmount = paymentRequest.ValorConsulta,
                Description = paymentRequest.NomeServico,
                PaymentMethodId = "pix",
                Payer = new PaymentPayerRequest
                {
                    Email = paymentRequest.EmailPagador,
                    FirstName = paymentRequest.NomePagador,
                    LastName = paymentRequest.SobrenomePagador,
                    Identification = new IdentificationRequest
                    {
                        Type = "CPF",
                        Number = paymentRequest.CPFPagador,
                    },
                },
                NotificationUrl = _configuration["Ngrok:NotificationUrl"]
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request, requestOptions);
            Console.WriteLine(payment);
            return payment;
        }
    }
}
