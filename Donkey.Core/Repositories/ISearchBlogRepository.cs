using Donkey.Core.Entities;
using Donkey.Core.Utilities.Pagination;

namespace Donkey.Core.Repositories
{
    public interface ISearchBlogRepository
    {
        public Task<PaginatedResult<Blog>> GetAll(string userEmail, int page, int size);
    }
}
