using Donkey.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Repositories
{
    public interface IPostsRepository
    {
        public Task<Guid> Create(Post post);
        public Task<Post> Get(Guid id);
        public Task Delete(Post post);
    }
}
