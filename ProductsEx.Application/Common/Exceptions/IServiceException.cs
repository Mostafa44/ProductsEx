using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductsEx.Application.Common.Errors
{
    public interface IServiceException
    {
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
    }
}