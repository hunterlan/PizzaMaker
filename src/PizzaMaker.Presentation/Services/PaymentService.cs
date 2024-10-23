using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation.Services;

public class PaymentService(PizzaContext context) : IPaymentService
{
    public IEnumerable<PaymentType> GetPaymentTypes()
    {
        return context.PaymentTypes.AsEnumerable();
    }
}