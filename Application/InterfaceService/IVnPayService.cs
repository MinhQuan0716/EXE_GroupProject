using Application.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public interface IVnPayService
    {
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        public PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
