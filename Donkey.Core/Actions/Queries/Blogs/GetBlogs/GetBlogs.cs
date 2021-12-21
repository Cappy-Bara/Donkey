using Donkey.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Queries.Blogs.GetBlogs
{
    public class GetBlogs : IRequest<IEnumerable<Blog>>
    {
        public string Email { get; set; }
    }
}
