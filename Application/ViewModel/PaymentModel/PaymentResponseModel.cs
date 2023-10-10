using Microsoft.Extensions.Primitives;

namespace Application.ViewModel.PaymentModel
{
    public class PaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public object OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public StringValues Token { get; set; }
        public object VnPayResponseCode { get; set; }
    }
}