﻿using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;

namespace Donkey.Core.Actions.Commands.Posts.Modify
{
    public class ModifyPostHandler : IRequestHandler<ModifyPost, Unit>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPostsRepository _postsRepo;

        public ModifyPostHandler(IPostsRepository postsRepo, IUsersRepository usersRepo)
        {
            _postsRepo = postsRepo;
            _usersRepo = usersRepo;
        }

        public async Task<Unit> Handle(ModifyPost request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.UserEmail);

            if (user is null)
                throw new NotFoundException("This user does not exist.");

            var post = await _postsRepo.Get(request.PostId);

            if (post is null)
                throw new NotFoundException("This post does not exist.");

            if (post.AuthorEmail != user.Email)
                throw new BadRequestException("This post does not belong to logged user.");

            post.Title = request.PostTitle;
            post.Content = request.PostContent;

            await _postsRepo.Update(post);

            return Unit.Value;
        }
    }
}
