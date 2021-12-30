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
        public Task<Blog> Get(string name);
        public Task Delete(Blog blog);
        public Task Update(Blog blog);
    }
}
