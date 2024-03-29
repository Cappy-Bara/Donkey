﻿using Donkey.Core.Actions.Queries.Account.Login;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Moq;
using System.Threading;
using Donkey.Core.Repositories;
using Donkey.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Donkey.Core.Exceptions;

namespace Donkey.Tests.Core.Accounts
{
    public class LoginHandlerTests
    {

        private static async Task<string> EvaluateLogin(string loginEmail, string loginPassword, string emailInDb, string passwordInDb)
        {
            var userInDb = new User(emailInDb, null);

            var hasher = new PasswordHasher<User>();
            var userInDbHash = hasher.HashPassword(userInDb, passwordInDb);
            userInDb.PasswordHash = userInDbHash;

            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.Get(emailInDb)).ReturnsAsync(new User(emailInDb, userInDbHash));
            IUsersRepository UserRepoMock = mock.Object;

            var sut = new LoginHandler(UserRepoMock, hasher);
            var query = new Login()
            {
                JwtKey = "DONKEY_SUPER_CMS_TESTS",
                JwtIssuer = "https://testowy-osiol.com",
                Email = loginEmail,
                ExpireDays = 1,
                Password = loginPassword
            };
            var output = await sut.Handle(query, CancellationToken.None);
            return output;
        }



        [Fact]
        public async Task LoginHander_UserProvidedValidData_ReturnsValidToken()
        {

            var sut = await EvaluateLogin("test@wp.pl","aaaaaa", "test@wp.pl", "aaaaaa");

            sut.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task LoginHander_UserProvidedInvalidPassword_ShouldThrowBadRequestException()
        {

            var sut = () => EvaluateLogin("test@wp.pl", "aaaaaa12", "test@wp.pl", "aaaaaa");

            await sut.Should().ThrowAsync<BadRequestException>();
        }
    }
}
