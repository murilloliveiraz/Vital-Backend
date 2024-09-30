using Application.Services.Interfaces;
using Domain;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;

namespace Application.Services.Classes
{
    public class CreditCardService : ICreditCardService
    {
        public async Task<Payment> CobrarComPix(CreditCardPayment request)
        {
            var requestOptions = new RequestOptions();
            string idempotencyKey = Guid.NewGuid().ToString();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idempotencyKey);

            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = request.TransactionAmount,
                Token = request.Token,
                Description = request.Description,
                Installments = request.Installments,
                PaymentMethodId = request.PaymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = request.Email,
                    Identification = new IdentificationRequest
                    {
                        Type = request.Type,
                        Number = request.Number,
                    },
                },
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest, requestOptions);
            return payment;
        }
    }
}
