using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Classes
{
    public class CreditCardService : ICreditCardService
    {
        private IConfiguration _configuration;

        public CreditCardService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Payment> CobrarComCartaoDeCredito(CreditCardPayment request)
        {
            var requestOptions = new RequestOptions();
            //GARANTE QUE CADA PAGAMENTO É UNICO
            string idempotencyKey = Guid.NewGuid().ToString();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idempotencyKey);

            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = request.ValorConsulta,
                Token = request.Token,
                Description = request.NomeServico,
                Installments = request.Installments,
                PaymentMethodId = request.PaymentMethodId,
                ExternalReference =  request.ConsultaId.ToString(),
                StatementDescriptor = "VITAL",
                NotificationUrl = _configuration["Ngrok:NotificationUrl"],
                Payer = new PaymentPayerRequest
                {
                    Email = request.EmailPagador,
                    FirstName = request.NomePagador,
                    LastName = request.NomePagador,
                    Identification = new IdentificationRequest
                    {
                        Type = request.Type,
                        Number = request.Number,
                    },
                },
                AdditionalInfo = new PaymentAdditionalInfoRequest
                {
                    Items = new List<PaymentItemRequest>()
                    {
                        new PaymentItemRequest()
                        {
                            CategoryId = request.NomeServico,
                            Id =  $"{request.ConsultaId.ToString()}-{request.NomeServico}",
                            Description = $"{request.NomeServico} na VITAL",
                            Quantity = 1,
                            Title = request.NomeServico,
                            UnitPrice = request.ValorConsulta
                        }
                    },
                }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest, requestOptions);
            Console.WriteLine(payment);
            return payment;
        }
    }
}
