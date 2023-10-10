using Application.Common;
using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.Uitls;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppConfiguration _configuration;
        private readonly ICurrentTime _currentTime;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ISendMailHelper _sendMailHelper;

        public UserService(IUnitOfWork unitOfWork, AppConfiguration configuration, ICurrentTime currentTime, IMapper mapper, IMemoryCache memoryCache, ISendMailHelper sendMailHelper)
        {
            _unitOfWork = unitOfWork;
            _configuration=configuration;
            _currentTime = currentTime;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sendMailHelper = sendMailHelper;
        }
        public async Task<Token> LoginAsync(LoginModel loginModel)
        {
            var user = await _unitOfWork.UserRepository.FindUserByEmail(loginModel.Email);
            if(user == null) 
            {
                throw new Exception("Account not found,please register first");
            }
            if (loginModel.Password.CheckPassword(user.Password)==false)
            {
                throw new Exception("Password incorrect");
            }
            var refreshToken = RefreshTokenString.GetRefreshToken();
            var accessToken = user.GenerateJsonWebToken(_configuration!.JWTSecretKey, _currentTime.GetCurrentTime());
            var expireRefreshTokenTime = DateTime.Now.AddHours(24);

            user.RefreshToken = refreshToken;
            user.ExpireTokenTime = expireRefreshTokenTime;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();
            return new Token
            {
                Username = user.UserName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RoleName=user.Role.RoleName
            };

        }
        public async Task<Token> LoginWithEmail(LoginWithEmailViewModel loginDto)
        {
            var user = await _unitOfWork.UserRepository.FindUserByEmail(loginDto.Email);
            if (user != null)
            {
                var refreshToken = RefreshTokenString.GetRefreshToken();
                var accessToken = user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime());
                var expireRefreshTokenTime = DateTime.Now.AddHours(24);

                user.RefreshToken = refreshToken;
                user.ExpireTokenTime = expireRefreshTokenTime;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveChangeAsync();
                return new Token
                {
                    Username = user.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RoleName=user.Role.RoleName
                };
            }
            return null;
        }
        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            bool isEmailExisted =await  _unitOfWork.UserRepository.CheckMailExisted(registerModel.Email);
            if(isEmailExisted)
            {
                throw new Exception("Mail already existed");
            }
            DateTime birthDay;
            if (!DateTime.TryParseExact(registerModel.BirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
            {
                throw new Exception("Invalid Birthday format. Please use 'yyyy-MM-dd' format.");
            }
            var newUser = new User
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                Password = registerModel.Password.Hash(),
                BirthDay = birthDay,
                RoleId =2,
                IsDelete= false,
            };
            await _unitOfWork.UserRepository.AddAsync(newUser); 
            return await _unitOfWork.SaveChangeAsync()>0;
        }
        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var result = _mapper.Map<List<UserViewModel>>(users);
            return result;
        }
        public async Task AddUserAsync(User user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> BanUser(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if(user== null)
            {
                throw new Exception("User not existed");
            }
            _unitOfWork.UserRepository.SoftRemove(user);
            return await _unitOfWork.SaveChangeAsync() > 0; ;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllUsers();
        }
        public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            string email;
            _ = _memoryCache.TryGetValue(resetPasswordDTO.Code, out email);

            if (email != null)
            {
                if (resetPasswordDTO.NewPassword.Equals(resetPasswordDTO.ConfirmPassword))
                {
                    var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                    if (user != null)
                    {
                        resetPasswordDTO.NewPassword = resetPasswordDTO.NewPassword.Hash();
                        _ = _mapper.Map(resetPasswordDTO, user, typeof(ResetPasswordDTO), typeof(User));
                        _unitOfWork.UserRepository.Update(user);
                        if (await _unitOfWork.SaveChangeAsync() > 0)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    throw new Exception("Password and Confirm Password is not match");
                }
            }
            throw new Exception("Not exsited Code");
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<string> SendResetPassword(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user != null)
            {
                var key = UserService.RandomString(6);
                //Get project's directory and fetch ForgotPasswordTemplate content from EmailTemplates
                string exePath = Environment.CurrentDirectory.ToString();
                if (exePath.Contains(@"\bin\Debug\net7.0"))
                    exePath = exePath.Remove(exePath.Length - (@"\bin\Debug\net7.0").Length);
                string FilePath = exePath + @"\EmailTemplates\ForgotPasswordTemplate.html";
                StreamReader streamreader = new StreamReader(FilePath);
                string MailText = streamreader.ReadToEnd();
                streamreader.Close();
                //Replace [resetpasswordkey] = key
                MailText = MailText.Replace("[resetpasswordkey]", key);
                //Replace [emailaddress] = email
                MailText = MailText.Replace("[emailaddress]", email);
                var result = await _sendMailHelper.SendMailAsync(email, "ResetPassword", MailText);
                if (!result) return string.Empty;

                _memoryCache.Set(key, email, DateTimeOffset.Now.AddMinutes(10));
                return key;
            }
            else
            {
                throw new Exception("User not available");
            }
        }
    }
}
