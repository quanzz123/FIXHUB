using FIXHUB.ViewModels;

namespace FIXHUB.Services
{
    public interface IVnpayServices
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
