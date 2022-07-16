using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsEx.Application.Common.Errors;
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
            Result<AuthenticationResult> registerResult = _authenticationService.Register(request.FirstName,
                                                                    request.LastName,
                                                                    request.Email,
                                                                    request.Password);
            if (registerResult.IsSuccess)
            {
                return Ok(MapToAuthResult(registerResult.Value));
            }
            var firstError = registerResult.Errors[0];
            if (firstError is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Actually, This Email Aleady Exists");
            }
            return Problem();
            // AuthenticationResponse response = MapToAuthResult();
            //  return Ok(response);
        }

        private static AuthenticationResponse MapToAuthResult(AuthenticationResult authenticationResult)
        {
            return new AuthenticationResponse(authenticationResult.User.Id,
                                                     authenticationResult.User.FirstName,
                                                     authenticationResult.User.LastName,
                                                     authenticationResult.User.Email,
                                                     authenticationResult.Token);
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