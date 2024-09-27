using Domain;
using MercadoPago.Resource.Payment;

namespace Application.Services.Interfaces
{
    public interface IPixService
    {
        Task<Payment> CobrarComPix(PixPayment paymentRequest);
    }
}
