using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsEx.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}