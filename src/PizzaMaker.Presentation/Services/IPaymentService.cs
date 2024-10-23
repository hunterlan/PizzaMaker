using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation.Services;

public interface IPaymentService
{
    IEnumerable<PaymentType> GetPaymentTypes();
}