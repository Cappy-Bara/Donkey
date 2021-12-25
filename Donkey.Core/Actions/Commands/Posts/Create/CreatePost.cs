using MediatR;

namespace Donkey.Core.Actions.Commands.Posts.Create
{
    public class CreatePost : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string BlogName { get; set; }
        public string AuthorEmail { get; set; }
    }
}
