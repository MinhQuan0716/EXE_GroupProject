using Application.InterfaceService;
using Application.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors]
    public class UserController : MainController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            bool isRegisterSuccess = await _userService.RegisterAsync(registerModel);
            if (!isRegisterSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        public async Task<Token> Login(LoginModel loginModel)
        {
            return await _userService.LoginAsync(loginModel);
        }
    }
}
