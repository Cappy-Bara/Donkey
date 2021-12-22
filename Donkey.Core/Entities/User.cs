using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Blog> Blogs { get; set; }

        private User()
        {

        }
        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
