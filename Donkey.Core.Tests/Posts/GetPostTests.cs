using Donkey.Core.Actions.Queries.Posts.GetPost;
using Donkey.Core.Entities;
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
    public class GetPostTests
    {


        private async Task<DonkeyDbContext> GetDbContext(List<Post> posts)
        {
            posts ??= new List<Post>();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
            return context;
        }

        private async Task<GetPostHandler> GetHandler(List<Post> posts = null)
        {
            var context = await GetDbContext(posts);
            var postsRepo = new PostsRepository(context);

            return new GetPostHandler(postsRepo);
        }

        [Fact]
        public async Task GetPost_PostExist_ReturnsValidPost()
        {
            var request = new GetPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78")
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

            var sus = await GetHandler(new List<Post>() {post});

            var output = await sus.Handle(request, CancellationToken.None);

            output.Id.Should().Be(post.Id);
        }

        [Fact]
        public async Task GetPost_PostDoesNotExist_ReturnsNull()
        {
            var request = new GetPost()
            {
                PostId = Guid.Parse("efd66a77-aa57-4fcd-8155-5e4ad1996e78")
            };

            var sus = await GetHandler();

            var output = await sus.Handle(request, CancellationToken.None);

            output.Should().BeNull();
        }
    }
}
