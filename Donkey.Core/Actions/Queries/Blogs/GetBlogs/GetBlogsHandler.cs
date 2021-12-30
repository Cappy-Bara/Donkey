using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using Donkey.Core.Utilities.Pagination;
using MediatR;

namespace Donkey.Core.Actions.Queries.Blogs.GetBlogs
{
    public class GetBlogsHandler : IRequestHandler<GetBlogs, PaginatedResult<Blog>>
    {
        private readonly ISearchBlogRepository _blogsRepo;
        private readonly IUsersRepository _usersRepo;

        public GetBlogsHandler(ISearchBlogRepository blogRepo, IUsersRepository usersRepo)
        {
            _blogsRepo = blogRepo;
            _usersRepo = usersRepo;
        }

        public async Task<PaginatedResult<Blog>> Handle(GetBlogs request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);
            if (user == null)
                throw new NotFoundException("This user does not exist.");

            var output = await _blogsRepo.GetAll(request.Email, request.PageNumber, request.PostsOnPageAmount);

            if (output == PaginatedResult<Blog>.Invalid)
                throw new BadRequestException("This page does not exist.");

            return await _blogsRepo.GetAll(request.Email, request.PageNumber, request.PostsOnPageAmount);
        }
    }
}
