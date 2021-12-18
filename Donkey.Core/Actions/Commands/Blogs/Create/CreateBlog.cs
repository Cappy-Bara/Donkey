using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Blogs
{
    public class CreateBlog : IRequest
    {
        public string UserEmail { get; set; }
        public string BlogName { get; set; }
    }
}
