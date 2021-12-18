using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Repositories
{
    public class MockedBlogsRepository : IBlogsRepository
    {
        public Task Create(Blog blog)
        {
            return Task.CompletedTask;
        }

        public Task Delete(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> Get(string name, string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetAll(string email)
        {
            throw new NotImplementedException();
        }

        public Task Update(Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
