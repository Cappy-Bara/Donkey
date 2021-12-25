using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Blogs.Delete
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlog>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IBlogsRepository _blogsRepo;
        public DeleteBlogHandler(IUsersRepository usersRepo, IBlogsRepository blogsRepo)
        {
            _usersRepo = usersRepo;
            _blogsRepo = blogsRepo;
        }

        public async Task<Unit> Handle(DeleteBlog request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);
            if (user == null)
                throw new NotFoundException("This user does not exist.");

            var blog = await _blogsRepo.Get(request.BlogName);
            if (blog.OwnerEmail != request.Email)
                throw new BadRequestException("This blog doesn't belong to logged user.");

            await _blogsRepo.Delete(blog);
            return new Unit();
        }
    }
}
