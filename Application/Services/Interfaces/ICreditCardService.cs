using Domain;
using MercadoPago.Resource.Payment;

namespace Application.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<Payment> CobrarComCartaoDeCredito(CreditCardPayment request);
    }
}
