using Donkey.Core.Actions.Queries.Posts.GetPost;
using Donkey.Core.Actions.Queries.Posts.GetPosts;
using Donkey.Core.Entities;
using Donkey.Core.Exceptions;
using Donkey.Core.Utilities.Pagination;
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
    public class GetPostsTests
    {
        private async Task<DonkeyDbContext> GetDbContext(List<Post> posts, List<Blog> blogs)
        {
            posts ??= new List<Post>();
            blogs ??= new List<Blog>();

            var options = new DbContextOptionsBuilder<DonkeyDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new DonkeyDbContext(options);
            await context.Posts.AddRangeAsync(posts);
            await context.Blogs.AddRangeAsync(blogs);
            await context.SaveChangesAsync();
            return context;
        }
        private async Task<GetPostsHandler> GetHandler(List<Post> posts = null, List<Blog> blogs= null)
        {
            var context = await GetDbContext(posts,blogs);
            var postsRepo = new SearchPostRepository(context);
            var blogsRepo = new BlogsRepository(context);

            return new GetPostsHandler(blogsRepo,postsRepo);
        }

        [Fact]
        public async Task GetPosts_BlogDoesNotExist_ThrowsException()
        {
            var request = new GetPosts
            {
                BlogName = "test",
                PageNumber = 1,
                PostsOnPageAmount = 5,
            };

            var sus = await GetHandler();

            var output = () => sus.Handle(request, CancellationToken.None);

            await output.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPost_NoPostsInBlog_ShouldReturnEmptyPagination()
        {
            var blogs = new List<Blog> { new Blog { Name = "test", OwnerEmail="test@wp.pl" } };

            var request = new GetPosts
            {
                BlogName = "test",
                PageNumber = 1,
                PostsOnPageAmount = 5,
            };

            var sus = await GetHandler(null,blogs);

            var output = await sus.Handle(request, CancellationToken.None);

            output.Should().Be(PaginatedResult<Post>.Empty);
        }

        [Fact]
        public async Task GetPost_SomePostsInBlogs_ShouldReturnValidLists()
        {
            var blogs = new List<Blog> { new Blog { Name = "test", OwnerEmail = "test@wp.pl" } };
            
            var posts = new List<Post>
            {
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
                new Post {AuthorEmail="test@wp.pl",BlogName="test",Content="aaaaaa",CreatedDate=DateTime.Now,Id=Guid.NewGuid(),Title="test"},
            };

            var request = new GetPosts
            {
                BlogName = "test",
                PageNumber = 1,
                PostsOnPageAmount = 3,
            };

            var sus = await GetHandler(posts, blogs);

            var output = await sus.Handle(request, CancellationToken.None);

            output.Items.Should().HaveCount(3);
            output.LastElementIndex.Should().Be(3);
            output.FirstElementIndex.Should().Be(1);
            output.AvailablePages.Should().Be(2);
        }
    }
}
