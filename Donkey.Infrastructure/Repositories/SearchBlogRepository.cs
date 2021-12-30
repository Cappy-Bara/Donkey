using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using Donkey.Core.Utilities.Pagination;
using Donkey.Infrastructure.Database;
using Donkey.Infrastructure.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Repositories
{
    public class SearchBlogRepository : ISearchBlogRepository
    {
        private readonly DonkeyDbContext _dbContext;

        public SearchBlogRepository(DonkeyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Blog>> GetAll(string userEmail, int page, int size)
        {
            var query = _dbContext.Blogs.Where(x => x.OwnerEmail == userEmail);

            var paginator = new Paginator<Blog>(query, size);

            return await paginator.GetElementsFromPage(page);
        }
    }
}
