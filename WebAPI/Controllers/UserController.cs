using Application;
using Application.InterfaceService;
using Application.Uitls;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<Token> Login(LoginModel loginModel)
        {
            return await _userService.LoginAsync(loginModel);
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
                RoleId=2,
                IsDelete= false,
            };

            var user = _userService.GetAllAsync().Result.SingleOrDefault(u => u.Email.Equals(newUser.Email));
            if (user == null)
            {
                await _userService.AddUserAsync(newUser);
            }

            var token = await _userService.LoginWithEmail(_mapper.Map<LoginWithEmailViewModel>(newUser));
            return Ok(token);
        }

        /*[HttpPost]
        public async Task<Token> ExternalLogin(LoginModel loginModel)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new User { Email = payload.Email, UserName = payload.Email };
                    await _userManager.CreateAsync(user);
                    //prepare and send an email for the email confirmation
                    await _userManager.AddToRoleAsync(user, "Viewer");
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                return BadRequest("Invalid External Authentication.");
            //check for the Locked out account
            var token = await _jwtHandler.GenerateToken(user);
            return Ok(new AuthResponse { Token = token, IsAuthSuccessful = true });
        }*/

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _userService.SendResetPassword(email);
            if (!result.IsNullOrEmpty())
            {
                return Ok();
            }
            else return BadRequest("Cannot find User");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetObj)
        {
            var result = await _userService.ResetPassword(resetObj);
            if (result)
            {
                return Ok(result);
            }
            else return BadRequest();
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> BanUser(Guid userId)
        {
            bool isBanned= await _userService.BanUser(userId);
            if(isBanned)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            List<User> users = await _userService.GetAllUsersAsync();
            if(users.Count == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }
    }
}
