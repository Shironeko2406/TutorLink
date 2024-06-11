using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ITutorService _tutorService;
        private readonly IAccountService _accountService;

        public AuthServices(IConfiguration configuration, ITutorService tutorService, IAccountService accountService)
        {
            _configuration = configuration;
            _tutorService = tutorService;
            _accountService = accountService;
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
                new Claim("UserName", user.Username),
                new Claim("Email", user.Email),
                new Claim("Role", user.RoleId.ToString()),
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
                    return token;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
