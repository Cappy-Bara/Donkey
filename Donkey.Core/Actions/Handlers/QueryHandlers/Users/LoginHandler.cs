using Donkey.Core.Actions.Requests.Queries.Users;
using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Donkey.Core.Actions.Handlers.QueryHandlers.Users
{
    public class LoginHandler : IRequestHandler<Login, List<Claim>>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPasswordHasher<User> _hasher;

        public LoginHandler(IUsersRepository usersRepo, IPasswordHasher<User> hasher)
        {
            _usersRepo = usersRepo;
            _hasher = hasher;
        }

        public async Task<List<Claim>> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);

            if (user == null)
                throw new NotFoundException("User with this email does not exist!");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid password!");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
    }
}
