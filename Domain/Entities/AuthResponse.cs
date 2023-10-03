using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool IsAuthSuccessful { get; set; }
    }
}
