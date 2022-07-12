using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsEx.Application.Common.Interfaces.Authentication;

namespace ProductsEx.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(),
            "firsttName",
            "lastName",
            email,
            "token");
        }

        public AuthenticationResult Register(string firsttName, string lastName, string email, string password)
        {
            var userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firsttName, lastName);
            return new AuthenticationResult(userId,
            firsttName,
            lastName,
            email,
            token);
        }
    }
}