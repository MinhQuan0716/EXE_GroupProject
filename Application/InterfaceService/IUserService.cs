using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public  interface IUserService
    {
        public Task<bool> RegisterAsync(RegisterModel registerModel);
        public Task<Token> LoginAsync(LoginModel loginModel);
    }
}
