using Application;
using Application.InterfaceService;
using Application.Uitls;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    public class UserController : MainController
    {
        private readonly IUserService _userService;
        private readonly JwtHandler _jwtHandler;
        private readonly IExternalAuthUtils _externalAuthUtils;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IExternalAuthUtils externalAuthUtils, IMapper mapper)
        {
            _userService = userService;
            _externalAuthUtils = externalAuthUtils;
            _mapper = mapper;
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
        public async Task<IActionResult> LoginWithGoogle(ExternalAuth externalAuthDto)
        {
            var payload = await _externalAuthUtils.VerifyGoogleToken(externalAuthDto);
            if (payload == null)
            {
                return BadRequest("Invalid external authentication");
            }
            var newUser = new User
            {
                Email = payload.Email,
                UserName = payload.Email,
            };

            var user = _userService.GetAllAsync().Result.SingleOrDefault(u => u.Email == newUser.Email);
            if (user == null)
            {
                await _userService.AddUserAsync(newUser);
            }

            var token = await _userService.LoginWithEmail(_mapper.Map<LoginWithEmailViewModel>(newUser));
            return Ok(token);
        }
        [HttpPost]
        public async Task<Token> Login(LoginModel loginModel)
        {
            return await _userService.LoginAsync(loginModel);
        }
    }
}
