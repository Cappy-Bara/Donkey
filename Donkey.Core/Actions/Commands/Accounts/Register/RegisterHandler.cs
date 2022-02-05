using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Donkey.Core.Actions.Commands.Accounts.Register
{
    public class RegisterHandler : IRequestHandler<Register>
    {
        private readonly IPasswordHasher<User> _hasher;
        private readonly IUsersRepository _usersRepo;

        public RegisterHandler(IUsersRepository usersRepo, IPasswordHasher<User> hasher)
        {
            _usersRepo = usersRepo;
            _hasher = hasher;
        }

        public async Task<Unit> Handle(Register request, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepo.Get(request.Email);
            if (existingUser != null)
                throw new ConflictException("User with this email already exists.");

            var user = new User() {Email = request.Email};

            var hash = _hasher.HashPassword(user, request.Password);
            user.ApplyPasswordHash(hash);

            await _usersRepo.Add(user);
            return Unit.Value;
        }
    }
}
