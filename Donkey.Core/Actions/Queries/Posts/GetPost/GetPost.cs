using Donkey.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Queries.Posts.GetPost
{
    public class GetPost : IRequest<Post>
    {
        public Guid PostId { get; set; }
    }
}
