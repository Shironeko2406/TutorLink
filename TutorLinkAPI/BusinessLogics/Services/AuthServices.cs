using AutoMapper;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
#pragma warning disable CS8601
namespace TutorLinkAPI.BusinessLogics.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ITutorService _tutorService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AuthServices(IConfiguration configuration, ITutorService tutorService, IAccountService accountService, IMapper mapper)
        {
            _configuration = configuration;
            _tutorService = tutorService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public AccessTokenViewModel GenerateToken(IUser user)
        {
            var jwtSettingsSection = _configuration.GetSection("JwtSettings");
            var securityKey = jwtSettingsSection["SecurityKey"];
            var issuer = jwtSettingsSection["Issuer"];
            var audience = jwtSettingsSection["Audience"];

            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email),
                new Claim("Role", user.RoleId.ToString()),
                new Claim("Fullname", user.Fullname)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AccessTokenViewModel()
            {
                AccessTokenToken = accessToken,
                ExpiredAt = token.ValidTo
            };
        }

        public AccessTokenViewModel Login(string username, string password)
        {

            try
            {
                IUser user = null;
                var account = _accountService.GetAccountEntityByUsername(username);
                if (account != null && account.Password == password)
                {
                    user = account;
                }
                else
                {
                    var tutor = _tutorService.GetTutorEntityByUsername(username);
                    if (tutor != null && tutor.Password == password)
                    {
                        user = tutor;
                    }
                }

                if (user != null)
                {
                    var token = GenerateToken(user);

                    var accessTokenViewModel = new AccessTokenViewModel
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        AccessTokenToken = token.AccessTokenToken,
                        ExpiredAt = token.ExpiredAt
                    };

                    return accessTokenViewModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AccessTokenViewModel> GoogleLogin(string idToken)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            var email = payload.Email;
            var name = payload.Name;

            var account = await _accountService.GetAccountByEmail(email);
            if (account == null)
            {
                var accountGoogleViewModel = new AccountGoogleViewModel
                {
                    AccountId = Guid.NewGuid(),
                    Email = email,
                    Fullname = name,
                    RoleId = 4,
                    Username = email,
                    Password = "",
                    Phone = "",
                    Address = "",
                    AvatarUrl = null,
                    Gender = UserGenders.Other
                };

                account = _mapper.Map<Account>(await _accountService.AddNewAccountGoogle(accountGoogleViewModel));
            }

            var token = GenerateToken(account);

            return new AccessTokenViewModel
            {
                UserId = account.AccountId,
                Username = account.Username,
                Email = account.Email,
                RoleId = account.RoleId,
                AccessTokenToken = token.AccessTokenToken,
                ExpiredAt = token.ExpiredAt
            };
        }
    }
}
