using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;

namespace Application.Services.Classes
{
    public class PixService : IPixService
    {
        public async Task<Payment> CobrarComPix(PixPayment paymentRequest)
        {
            var requestOptions = new RequestOptions();
            string idempotencyKey = Guid.NewGuid().ToString();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idempotencyKey);
            var request = new PaymentCreateRequest
            {
                TransactionAmount = paymentRequest.Valor,
                Description = paymentRequest.Servico,
                PaymentMethodId = "pix",
                Payer = new PaymentPayerRequest
                {
                    Email = paymentRequest.Email,
                    FirstName = paymentRequest.Nome,
                    LastName = paymentRequest.Sobrenome,
                    Identification = new IdentificationRequest
                    {
                        Type = "CPF",
                        Number = paymentRequest.CPF,
                    },
                },
                NotificationUrl = paymentRequest.NotificationURL
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request, requestOptions);
            return payment;
        }
    }
}
