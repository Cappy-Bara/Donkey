using Donkey.Core.Entities;
using Donkey.Core.Utilities.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Queries.Posts.GetPosts
{
    public class GetPosts : IRequest<PaginatedResult<Post>>
    {
        public string BlogName { get; set; }
        public int PageNumber { get; set; }
        public int PostsOnPageAmount { get; set; }
    }
}
