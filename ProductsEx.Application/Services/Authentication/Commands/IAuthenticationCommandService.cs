using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using ProductsEx.Application.Services.Authentication.Common;

namespace ProductsEx.Application.Services.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {

        Result<AuthenticationResult> Register(string firsttName, string lastName, string email, string password);
    }
}