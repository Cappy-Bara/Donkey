using Donkey.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Repositories
{
    public interface IUsersRepository
    {
        public Task<User> Get(string email);
        public Task Add(User user);
    }
}
