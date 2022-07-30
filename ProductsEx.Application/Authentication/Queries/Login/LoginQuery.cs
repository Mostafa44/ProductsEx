using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProductsEx.Application.Authentication.Common;

namespace ProductsEx.Application.Authentication.Queries.Login
{
    public record LoginQuery(

         string Email,
         string Password
     ) : IRequest<AuthenticationResult>;
}