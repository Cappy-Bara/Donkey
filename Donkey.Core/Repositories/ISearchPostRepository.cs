using Donkey.Core.Entities;
using Donkey.Core.Utilities.Pagination;

namespace Donkey.Core.Repositories
{
    public interface ISearchPostRepository
    {
        public Task<PaginatedResult<Post>> GetFromBlog(string name, int page, int size);
    }
}
