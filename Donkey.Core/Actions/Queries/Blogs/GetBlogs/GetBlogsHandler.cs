using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;

namespace Donkey.Core.Actions.Queries.Blogs.GetBlogs
{
    public class GetBlogsHandler : IRequestHandler<GetBlogs, IEnumerable<Blog>>
    {
        private readonly IBlogsRepository _blogsRepo;
        private readonly IUsersRepository _usersRepo;

        public GetBlogsHandler(IBlogsRepository blogRepo, IUsersRepository usersRepo)
        {
            _blogsRepo = blogRepo;
            _usersRepo = usersRepo;
        }

        public async Task<IEnumerable<Blog>> Handle(GetBlogs request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);
            if (user == null)
                throw new NotFoundException("This user does not exist.");

            return await _blogsRepo.GetAll(request.Email);
        }
    }
}
