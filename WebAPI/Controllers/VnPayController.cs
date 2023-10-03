using Application.InterfaceService;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class VnPayController : MainController
    {
        private readonly IVnPayService _vnPayService;
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
            //return Ok(url);
        }

        [HttpGet("PaymentCallback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(response);
        }

        /*public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }*/
    }
}
