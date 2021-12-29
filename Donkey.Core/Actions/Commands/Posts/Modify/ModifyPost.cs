using MediatR;

namespace Donkey.Core.Actions.Commands.Posts.Modify
{
    public class ModifyPost : IRequest
    {
        public Guid PostId { get; set; }
        public string UserEmail { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
    }
}
