using Donkey.Core.Entities;
using Donkey.Core.Utilities.Pagination;
using MediatR;

namespace Donkey.Core.Actions.Queries.Blogs.GetBlogs
{
    public class GetBlogs : IRequest<PaginatedResult<Blog>>
    {
        public string Email { get; set; }
        public int PageNumber { get; set; }
        public int PostsOnPageAmount { get; set; }
    }
}
