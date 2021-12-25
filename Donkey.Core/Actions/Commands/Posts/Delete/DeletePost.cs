using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Posts.Delete
{
    public class DeletePost : IRequest
    {
        public Guid PostId { get; set; }
        public string UserEmail { get; set; }
    }
}
