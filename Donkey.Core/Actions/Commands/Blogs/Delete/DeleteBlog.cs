using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Blogs.Delete
{
    public class DeleteBlog : IRequest
    {
        public string BlogName { get; set; }
        public string Email { get; set; }
    }
}
