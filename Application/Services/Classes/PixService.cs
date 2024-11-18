using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;
using Application.Helpers;

namespace Application.Services.Classes
{
    public class PixService : IPixService
    {
        private IConfiguration _configuration;
        private readonly IEmailService _emailSender;

        public PixService(IConfiguration configuration, IEmailService emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
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
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = paymentRequest.EmailPagador,
                Subject = "Recebemos o seu pagamento!",
                Body = CommunicationEmail.PaymentReceived()
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return payment;
        }
    }
}
