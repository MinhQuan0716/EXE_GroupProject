using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.PaymentModel
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public string CustomerName { get; set; }
        public double Cost { get; set; }
        public string OrderDescription { get; set; }
    }
}
