using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Posts
{
    public class CreatePost : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string BlogName { get; set; }
        public string AuthorEmail { get; set; }
    }
}
