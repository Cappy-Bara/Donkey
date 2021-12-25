using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using Donkey.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly DonkeyDbContext _dbContext;
        public PostsRepository(DonkeyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Create(Post post)
        {
            var output = _dbContext.Posts.Add(post).Entity.Id;
            await _dbContext.SaveChangesAsync();
            return output;
        }

        public async Task<Post> Get(Guid id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
