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
    public class BlogsRepository : IBlogsRepository
    {
        private readonly DonkeyDbContext _dbContext;

        public BlogsRepository(DonkeyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Blog blog)
        {
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(string name)
        {
            var blog = await Get(name);
            _dbContext.Blogs.Remove(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Blog> Get(string name)
        {
            return await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Blog>> GetAll(string email)
        {
            return await _dbContext.Blogs.Where(x => x.OwnerEmail == email).ToListAsync();
        }

        public async Task Update(Blog blog)
        {
            _dbContext.Update(blog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
