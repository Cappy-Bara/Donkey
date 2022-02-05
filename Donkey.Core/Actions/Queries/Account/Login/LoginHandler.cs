using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Donkey.Core.Actions.Queries.Account.Login
{
    public class LoginHandler : IRequestHandler<Login, string>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPasswordHasher<User> _hasher;

        public LoginHandler(IUsersRepository usersRepo, IPasswordHasher<User> hasher)
        {
            _usersRepo = usersRepo;
            _hasher = hasher;
        }

        public async Task<string> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);

            VerifyUser(user, request.Password);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = GenerateToken(claims,request);

            var output = new JwtSecurityTokenHandler().WriteToken(token);

            return output;
        }
        private void VerifyUser(User user, string providedPassword)
        {
            if (user == null)
                throw new NotFoundException("User with this email does not exist!");

            if (!user.IsActive)
                throw new BadRequestException("Account is not activated.");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid password!");
        }
        private static JwtSecurityToken GenerateToken(List<Claim> claims,Login loginData)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(loginData.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(loginData.ExpireDays);

            return new JwtSecurityToken(loginData.JwtIssuer, loginData.JwtIssuer, claims, expires: expires, signingCredentials: cred);
        }


    }
}
