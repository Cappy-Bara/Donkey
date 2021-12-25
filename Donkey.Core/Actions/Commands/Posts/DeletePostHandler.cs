using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Posts
{
    public class DeletePostHandler : IRequestHandler<DeletePost, Unit>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPostsRepository _postsRepo;

        public DeletePostHandler(IPostsRepository postsRepo, IUsersRepository usersRepo)
        {
            _postsRepo = postsRepo;
            _usersRepo = usersRepo;
        }

        public async Task<Unit> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.UserEmail);
            if (user is null)
                throw new NotFoundException("This User does not exist.");

            var post = await _postsRepo.Get(request.PostId);
            if (post is null)
                throw new NotFoundException("This post does not exist.");

            if (post.AuthorEmail != request.UserEmail)
                throw new BadRequestException("This post does not belong to this user.");

            await _postsRepo.Delete(post);
            return Unit.Value;
        }
    }
}
