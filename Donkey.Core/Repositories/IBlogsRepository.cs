using Donkey.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Repositories
{
    public interface IBlogsRepository
    {
        public Task Create(Blog blog);
        public Task<List<Blog>> GetAll(string email);
        public Task<Blog> Get(string name, string email);
        public Task Delete(string name);
        public Task Update(Blog blog);
    }
}
