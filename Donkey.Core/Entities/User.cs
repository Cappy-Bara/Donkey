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
        public bool IsActive { get; set; } = false;

        public User()
        {

        }
        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public void ApplyPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}
