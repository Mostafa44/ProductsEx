using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsEx.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
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
            return new AuthenticationResult(Guid.NewGuid(),
            firsttName,
            lastName,
            email,
            "token");
        }
    }
}