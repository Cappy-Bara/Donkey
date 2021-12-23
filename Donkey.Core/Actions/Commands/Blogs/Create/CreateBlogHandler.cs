using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Blogs.Create
{
    public class CreateBlogHandler : IRequestHandler<CreateBlog>
    {
        private readonly IBlogsRepository _blogsRepo;
        private readonly IUsersRepository _usersRepo;

        public CreateBlogHandler(IUsersRepository usersRepo, IBlogsRepository blogsRepo)
        {
            _usersRepo = usersRepo;
            _blogsRepo = blogsRepo;
        }

        public async Task<Unit> Handle(CreateBlog request, CancellationToken cancellationToken)
        {
            await ValidateUserExistance(request.UserEmail);

            await CheckBlogExistance(request.BlogName);

            var data = new Blog()
            {
                OwnerEmail = request.UserEmail,
                Name = request.BlogName
            };

            await _blogsRepo.Create(data);
            return Unit.Value;
        }
        private async Task ValidateUserExistance(string userEmail)
        {
            var user = await _usersRepo.Get(userEmail);
            if (user is null)
                throw new NotFoundException("This user does not exist");
        }
        private async Task CheckBlogExistance(string blogName)
        {
            var blog = await _blogsRepo.Get(blogName);
            if (blog is not null)
                throw new BadRequestException("Blog with this name already exists.");
        }
    }
}
