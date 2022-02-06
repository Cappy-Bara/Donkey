using Donkey.Core.Actions.Commands.Accounts.Register;
using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using Donkey.Infrastructure.Database;
using Donkey.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Donkey.Tests.Core.Accounts
{
    public class RegisterHandlerTests
    {
        private static async Task<IUsersRepository> EvaluateRegister(string userInDbEmail,string newUserEmail,string newUserPassword)
        {
            var userInDb = new User(userInDbEmail, null);

            var hasher = new PasswordHasher<User>();
            var userInDbHash = hasher.HashPassword(userInDb, "aaaaaa");
            userInDb.PasswordHash = userInDbHash;

            var context = await GetDbContext(userInDb);
            var usersRepo = new UsersRepository(context);

            var sut = new RegisterHandler(usersRepo, hasher);
            var query = new Register(newUserEmail, newUserPassword);

            await sut.Handle(query, CancellationToken.None);
            
            return usersRepo;
        }

        private async static Task<DonkeyDbContext> GetDbContext(User users)
        {
            users ??= new User();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Users.AddAsync(users);
            await context.SaveChangesAsync();
            return context;
        }


        [Fact]
        public async Task RegisterHandler_EmailIsTaken_ShouldThrowException() 
        {
            var sus = () => EvaluateRegister("test@wp.pl", "test@wp.pl", "aaaaaaa");

            await sus.Should().ThrowAsync<ConflictException>();
        }        
        
        [Fact]
        public async Task RegisterHandler_EmailIsFree_ShoudCreateUser() 
        {
            var repo = await EvaluateRegister("test@wp.pl", "test2@wp.pl", "aaaaaaa");

            var sus = await repo.Get("test2@wp.pl");

            sus.Should().NotBeNull();
            sus.Email.Should().Be("test2@wp.pl");
        }
    }
}
