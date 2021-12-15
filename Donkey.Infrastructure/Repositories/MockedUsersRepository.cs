using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Repositories
{
    public class MockedUsersRepository : IUsersRepository
    {
        public Task Add(User user)
        {
            return Task.CompletedTask;
        }

        public Task<User> Get(string email)
        {
            return Task.FromResult(new User("karol@wp.pl", "AQAAAAEAACcQAAAAEKEKeFhxBzleCU2b55FGzjpvwE87SuS0i09UtHXJF2V5iTJzrYLLJkY69vSkycb3qg=="));
        }
    }
}
