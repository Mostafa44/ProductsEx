using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        void AddUser(User user);
    }
}