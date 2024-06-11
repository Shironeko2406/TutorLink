﻿using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;   
        }
        
        #region Generate Token
        [NonAction]
        public IActionResult GenerateToken([FromBody] object user)
        {
            IUser iUser;

            if (user is Account account)
            {
                iUser = account;
            }
            else if (user is Tutor tutor)
            {
                iUser = tutor;
            }
            else
            {
                return BadRequest("Invalid user type.");
            }

            var tokenViewModel = _authService.GenerateToken(iUser);
            return Ok(tokenViewModel);
        }
        #endregion
        
        #region Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null || string.IsNullOrWhiteSpace(loginViewModel.Username) || string.IsNullOrWhiteSpace(loginViewModel.Password))
            {
                return BadRequest(new APIResponseViewModel
                {
                    Success = false,
                    Message = "Invalid login request",
                    Data = null
                });
            }

            var token = _authService.Login(loginViewModel.Username, loginViewModel.Password);
            if (token == null)
            {
                return Unauthorized(new APIResponseViewModel
                {
                    Success = false,
                    Message = "Invalid username or password",
                    Data = null
                });
            }

            return Ok(new APIResponseViewModel
            {
                Success = true,
                Message = "Login successful",
                Data = token
            });
        }
        #endregion
        
        #region Logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _logger.LogInformation("User logged out at {Time}", DateTime.UtcNow);
            return Ok(new APIResponseViewModel
            {
                Success = true,
                Message = "Logout successful",
                Data = null
            });
        }
        #endregion
    }
}
