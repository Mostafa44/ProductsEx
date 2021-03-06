using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace ProductsEx.Application.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string email, string password);
        Result<AuthenticationResult> Register(string firsttName, string lastName, string email, string password);
    }
}