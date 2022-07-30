using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace ProductsEx.Application.Services.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {

        Result<AuthenticationResult> Register(string firsttName, string lastName, string email, string password);
    }
}