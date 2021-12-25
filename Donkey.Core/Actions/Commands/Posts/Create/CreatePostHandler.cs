using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;

namespace Donkey.Core.Actions.Commands.Posts.Create
{
    public class CreatePostHandler : IRequestHandler<CreatePost, Guid>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPostsRepository _postsRepo;
        private readonly IBlogsRepository _blogsRepo;

        public CreatePostHandler(IBlogsRepository blogsRepo, IPostsRepository postsRepo, IUsersRepository usersRepo)
        {
            _blogsRepo = blogsRepo;
            _postsRepo = postsRepo;
            _usersRepo = usersRepo;
        }

        public async Task<Guid> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.AuthorEmail);
            if (user == null)
                throw new NotFoundException("This user does not exist.");

            var blog = await _blogsRepo.Get(request.BlogName);
            if (blog == null)
                throw new NotFoundException("This blog does not exist.");

            if (blog.OwnerEmail != request.AuthorEmail)
                throw new BadRequestException("This blog does not belong to this user.");

            var post = new Post()
            {
                AuthorEmail = request.AuthorEmail,
                BlogName = request.BlogName,
                CreatedDate = DateTime.UtcNow,
                Content = request.Content,
                Title = request.Title
            };

            var output = await _postsRepo.Create(post);

            return output;
        }
    }
}
