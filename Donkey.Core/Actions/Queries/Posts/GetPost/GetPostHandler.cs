using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Queries.Posts.GetPost
{
    public class GetPostHandler : IRequestHandler<GetPost, Post>
    {
        private readonly IPostsRepository _postsRepo;

        public GetPostHandler(IPostsRepository postsRepo)
        {
            _postsRepo = postsRepo;
        }

        public async Task<Post> Handle(GetPost request, CancellationToken cancellationToken)
        {
            var post = await _postsRepo.Get(request.PostId);
            return post;
        }
    }
}
