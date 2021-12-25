using Donkey.Core.Actions.Commands.Posts;
using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Repositories;
using Donkey.Infrastructure.Database;
using Donkey.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Donkey.Tests.Core.Posts
{
    public class DeletePostTests
    {
        private async Task<DonkeyDbContext> GetDbContext(List<Post> posts, List<User> users)
        {
            posts ??= new List<Post>();
            users ??= new List<User>();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Users.AddRangeAsync(users);
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
            return context;
        }

        private async Task<DeletePostHandler> GetHandler(List<Post> posts = null, List<User> users = null)
        {
            var context = await GetDbContext(posts, users);
            var usersRepo = new UsersRepository(context);
            var postsRepo = new PostsRepository(context);

            return new DeletePostHandler(postsRepo, usersRepo);
        }

        [Fact]
        public async Task DeletePostHandler_UserProvidedValidData_RemovesPost()
        {
            var users = new List<User>() { new User("test@mail.pl", "some_hash") };
            var posts = new List<Post>() { new Post() {
                AuthorEmail= "test@mail.pl",
                Id = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                BlogName = "nazwa bloga",
                CreatedDate = DateTime.Now,
                Content = "jakiś testowy kontent",
                Title = "Tytuł"
            } };

            var command = new DeletePost()
            {
                PostId = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                UserEmail = "test@mail.pl",
            };

            var request = await GetHandler(posts, users);

            var sus = () => request.Handle(command, CancellationToken.None);
            await sus.Should().NotThrowAsync();
        }

        [Fact]
        public async Task DeletePostHandler_PostDoesntExist_ShouldThrowException()
        {
            var users = new List<User>() { new User("test@mail.pl", "some_hash") };

            var command = new DeletePost()
            {
                PostId = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                UserEmail = "test@mail.pl",
            };

            var request = await GetHandler(null, users);

            var sus = () => request.Handle(command, CancellationToken.None);

            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeletePostHandler_UserDoesntExist_ShouldThrowException()
        {
            var command = new DeletePost()
            {
                PostId = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                UserEmail = "test@mail.pl",
            };

            var request = await GetHandler();

            var sus = () => request.Handle(command, CancellationToken.None);
            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeletePostHandler_PostDoesntBelongToUser_ShouldThrowException()
        {
            var users = new List<User>() { new User("test@mail.pl", "some_hash") };
            var posts = new List<Post>() { new Post() {
                AuthorEmail= "test2@mail.pl",
                Id = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                BlogName = "nazwa bloga",
                CreatedDate = DateTime.Now,
                Content = "jakiś testowy kontent",
                Title = "Tytuł"
            } };

            var command = new DeletePost()
            {
                PostId = Guid.Parse("dc4c94ae-5ea3-43a8-a5a1-3bd876100c1e"),
                UserEmail = "test@mail.pl",
            };

            var request = await GetHandler(posts,users);

            var sus = () => request.Handle(command, CancellationToken.None);
            await sus.Should().ThrowAsync<BadRequestException>();
        }

    }
}
