using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsEx.Application.Persistence;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {

        private static List<User> _users = new();
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}