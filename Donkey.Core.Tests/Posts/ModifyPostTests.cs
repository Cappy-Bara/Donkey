using Donkey.Core.Actions.Commands.Posts.Modify;
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
    public class ModifyPostTests
    {
        private async Task<DonkeyDbContext> GetDbContext(List<Post> posts, List<User> users)
        {
            posts ??= new List<Post>();
            users ??= new List<User>();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Posts.AddRangeAsync(posts);
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
            return context;
        }
        private async Task<ModifyPostHandler> GetHandler(List<Post> posts = null, List<User> users = null)
        {
            var context = await GetDbContext(posts,users);
            var postsRepo = new PostsRepository(context);
            var usersRepo = new UsersRepository(context);

            return new ModifyPostHandler(postsRepo,usersRepo);
        }

        [Fact]
        public async Task GetPost_PostExist_ModifiesPost()
        {
            var request = new ModifyPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                PostContent = "taeaes",
                PostTitle = "dddd",
                UserEmail = "testowy@mail.pl"
            };

            var post = new Post()
            {
                AuthorEmail = "testowy@mail.pl",
                BlogName = "test",
                Content = "testuje post blablah sialala dużo różnych słów",
                CreatedDate = DateTime.Now,
                Id = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                Title = "Testowy post",
            };

            var user = new User("testowy@mail.pl", "sfsd");

            var handler = await GetHandler(new List<Post> { post }, new List<User> { user });

            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetPost_UserDoesNotExist_ShouldThrowException()
        {
            var request = new ModifyPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                PostContent = "taeaes",
                PostTitle = "dddd",
                UserEmail = "testowy@mail.pl"
            };

            var post = new Post()
            {
                AuthorEmail = "testowy@mail.pl",
                BlogName = "test",
                Content = "testuje post blablah sialala dużo różnych słów",
                CreatedDate = DateTime.Now,
                Id = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                Title = "Testowy post",
            };

            var handler = await GetHandler(new List<Post> { post }, null);

            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPost_PostDoesNotExist_ShouldThrowException()
        {
            var request = new ModifyPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                PostContent = "taeaes",
                PostTitle = "dddd",
                UserEmail = "testowy@mail.pl"
            };

            var user = new User("testowy@mail.pl", "sfsd");

            var handler = await GetHandler(null, new List<User> { user });

            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPost_PostDoesNotBelongToUser_ShouldThrowException()
        {
            var request = new ModifyPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                PostContent = "taeaes",
                PostTitle = "dddd",
                UserEmail = "test@mail.pl"
            };

            var post = new Post()
            {
                AuthorEmail = "testowy@mail.pl",
                BlogName = "test",
                Content = "testuje post blablah sialala dużo różnych słów",
                CreatedDate = DateTime.Now,
                Id = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78"),
                Title = "Testowy post",
            };

            var user = new User("test@mail.pl", "sfsd");

            var handler = await GetHandler(new List<Post> { post }, new List<User> { user });

            var sus = () => handler.Handle(request, CancellationToken.None);

            await sus.Should().ThrowAsync<BadRequestException>();
        }
    }
}
