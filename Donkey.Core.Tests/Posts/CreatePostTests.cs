using Donkey.Core.Actions.Commands.Posts;
using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Infrastructure.Database;
using Donkey.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Donkey.Tests.Core.Posts
{
    public class CreatePostTests
    {

        private async Task<DonkeyDbContext> GetDbContext(List<Post> posts, List<User> users, List<Blog> blogs)
        {
            posts ??= new List<Post>();
            users ??= new List<User>();
            blogs ??= new List<Blog>();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Users.AddRangeAsync(users);
            await context.Blogs.AddRangeAsync(blogs);
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
            return context;
        }

        private async Task<CreatePostHandler> GetHandler(List<Post> posts = null, List<User> users = null,
            List<Blog> blogs = null)
        {
            var context = await GetDbContext(posts, users, blogs);
            var usersRepo = new UsersRepository(context);
            var postsRepo = new PostsRepository(context);
            var blogsRepo = new BlogsRepository(context);

            return new CreatePostHandler(blogsRepo, postsRepo, usersRepo);
        }


        [Fact]
        public async Task CreatePostHandler_UserProvidedValidData_ReturnsValidPostId()
        {

            var request = new CreatePost()
            {
                AuthorEmail = "test@mail.pl",
                Content = "test content",
                BlogName = "blog",
                Title = "Test title",
            };

            var users = new List<User>() { new User("test@mail.pl","some_hash")};
            var blogs = new List<Blog>() { new Blog { Name = "blog", OwnerEmail = "test@mail.pl" } };

            var sus = await GetHandler(null,users,blogs);

            var output = await sus.Handle(request, CancellationToken.None);

            output.Should().NotBeEmpty();
            output.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CreatePostHandler_BlogDoesntExist_ShouldThrowException()
        {

            var request = new CreatePost()
            {
                AuthorEmail = "test@mail.pl",
                Content = "test content",
                BlogName = "blog",
                Title = "Test title",
            };

            var users = new List<User>() { new User("test@mail.pl", "some_hash") };

            var handler = await GetHandler(null, users, null);
            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreatePostHandler_UserDoesntExist_ShouldThrowException()
        {

            var request = new CreatePost()
            {
                AuthorEmail = "test@mail.pl",
                Content = "test content",
                BlogName = "blog",
                Title = "Test title",
            };

            var blogs = new List<Blog>() { new Blog { Name = "blog", OwnerEmail = "test@mail.pl" } };

            var handler = await GetHandler(null, null, blogs);
            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreatePostHandler_BlogDoesntBelongToUser_ShouldThrowException()
        {

            var request = new CreatePost()
            {
                AuthorEmail = "test2@mail.pl",
                Content = "test content",
                BlogName = "blog",
                Title = "Test title",
            };

            var blogs = new List<Blog>() { new Blog { Name = "blog", OwnerEmail = "test@mail.pl" } };
            var users = new List<User>() { new User("test@mail.pl", "some_hash"), new User("test2@mail.pl", "some_hash") };


            var handler = await GetHandler(null, users, blogs);
            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<BadRequestException>();
        }
    }
}
