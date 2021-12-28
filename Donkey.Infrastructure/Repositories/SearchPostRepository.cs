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
    public class SearchPostRepository : ISearchPostRepository
    {
        private readonly DonkeyDbContext _dbContext;

        public SearchPostRepository(DonkeyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Post>> GetFromBlog(string name, int page, int size)
        {
            var query = _dbContext.Posts.Where(x => x.BlogName == name);

            var paginator = new Paginator<Post>(query, size);

            return await paginator.GetElementsFromPage(page);
        }
    }
}
