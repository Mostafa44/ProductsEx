using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsEx.Application.Services;
using ProductsEx.Contracts.Authentication;

namespace ProductsEx.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger,
                                         IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authenticationResult = _authenticationService.Register(request.FirstName,
                                                                    request.LastName,
                                                                    request.Email,
                                                                    request.Password);
            var response = new AuthenticationResponse(authenticationResult.User.Id,
                                                     authenticationResult.User.FirstName,
                                                     authenticationResult.User.LastName,
                                                     authenticationResult.User.Email,
                                                     authenticationResult.Token);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authenticationResult = _authenticationService.Login(request.Email,
                                                                   request.Password);
            var response = new AuthenticationResponse(authenticationResult.User.Id,
                                                  authenticationResult.User.FirstName,
                                                  authenticationResult.User.LastName,
                                                  authenticationResult.User.Email,
                                                  authenticationResult.Token);
            return Ok(response);
        }
    }
}