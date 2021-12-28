using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using Donkey.Core.Utilities.Pagination;
using MediatR;

namespace Donkey.Core.Actions.Queries.Posts.GetPosts
{
    public class GetPostsHandler : IRequestHandler<GetPosts, PaginatedResult<Post>>
    {
        private readonly ISearchPostRepository _postsRepo;
        private readonly IBlogsRepository _blogsRepo;
        public GetPostsHandler(IBlogsRepository blogsRepo, ISearchPostRepository postsRepo)
        {
            _blogsRepo = blogsRepo;
            _postsRepo = postsRepo;
        }

        public async Task<PaginatedResult<Post>> Handle(GetPosts request, CancellationToken cancellationToken)
        {
            var output = await _postsRepo.GetFromBlog(request.BlogName, request.PageNumber, request.PostsOnPageAmount);

            var blog = _blogsRepo.Get(request.BlogName);
            if (blog == null)
                throw new NotFoundException("This blog does not exist");

            if (output == PaginatedResult<Post>.Invalid)
                throw new BadRequestException("This page does not exist.");

            return output;
        }
    }
}
