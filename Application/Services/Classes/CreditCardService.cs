using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MercadoPago.Error;
using Application.Helpers;

namespace Application.Services.Classes
{
    public class CreditCardService : ICreditCardService
    {
        private IConfiguration _configuration;
        private readonly IEmailService _emailSender;

        public CreditCardService(IConfiguration configuration, IEmailService emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<Payment> CobrarComCartaoDeCredito(CreditCardPayment request)
        {
            var requestOptions = new RequestOptions();
            //GARANTE QUE CADA PAGAMENTO É UNICO
            string idempotencyKey = Guid.NewGuid().ToString();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idempotencyKey);

            var item = new PaymentItemRequest
            {
                CategoryId = request.NomeServico,
                Id = $"{request.ConsultaId.ToString()}-{request.NomeServico}",
                Description = $"{request.NomeServico} na VITAL",
                Quantity = 1,
                Title = request.NomeServico,
                UnitPrice = (decimal?)request.ValorConsulta,
                EventDate = DateTime.Now,
                Warranty = false,
            };

            var payerInfo = new PaymentAdditionalInfoPayerRequest
            {
                FirstName = request.NomePagador,
                LastName = request.SobrenomePagador,
            };

            var additionalInfo = new PaymentAdditionalInfoRequest
            {
                Items = new List<PaymentItemRequest> { item },
                Payer = payerInfo
            };

            var paymentPayerRequest = new PaymentPayerRequest
            {
                Email = request.EmailPagador,
                FirstName = request.NomePagador,
                LastName = request.SobrenomePagador,
                Identification = new IdentificationRequest
                {
                    Type = "CPF",
                    Number = request.Number,
                },
            };

            var paymentRequest = new PaymentCreateRequest
            {
                ApplicationFee = null,
                BinaryMode = false,
                CampaignId = null,
                Capture = false,
                CouponAmount = null,
                Description = request.NomeServico,
                DifferentialPricingId = null,
                Installments = 1,
                Metadata = null,
                NotificationUrl = _configuration["Ngrok:NotificationUrl"],
                Payer = paymentPayerRequest,
                PaymentMethodId = request.PaymentMethodId,
                ExternalReference = request.ConsultaId.ToString(),
                StatementDescriptor = "VITAL",
                TransactionAmount = (decimal?)request.ValorConsulta,
                Token = request.Token,
                AdditionalInfo = additionalInfo,
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest, requestOptions);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = paymentPayerRequest.Email,
                Subject = "Recebemos o seu pagamento!",
                Body = CommunicationEmail.PaymentReceived()
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return payment;
        }
    }
}
