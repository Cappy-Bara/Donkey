using Donkey.API.ClientDataProviders;
using Donkey.API.DTOs.Requests;
using Donkey.API.DTOs.Responses;
using Donkey.Core.Actions.Commands.Posts;
using Donkey.Core.Actions.Commands.Posts.Create;
using Donkey.Core.Actions.Commands.Posts.Delete;
using Donkey.Core.Actions.Queries.Posts.GetPost;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Donkey.API.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IUserDataProvider _userDataProvider;

        public PostsController(IMediator mediator, IUserDataProvider userDataProvider) : base(mediator)
        {
            _userDataProvider = userDataProvider;
        }

        [SwaggerOperation("Creates new post for chosen blog.")]
        [SwaggerResponse(201, "User provided correct post creation data.")]
        [SwaggerResponse(404, "Blog or user does not exsist.", typeof(ResponseDetails))]
        [SwaggerResponse(400, "Blog doesnt belong to logged user.", typeof(ResponseDetails))]
        [SwaggerResponse(401, "User is unauthorized", typeof(ResponseDetails))]
        [SwaggerResponse(400, "User didin't provided value in one of the fields, or provided incorrect value.", typeof(ValidationProblemDetails))]
        [HttpPost("/api/blogs/{blogId}/posts")]
        public async Task<ActionResult> Create([FromBody]CreatePostDto dto, [FromRoute] string blogId)
        {
            var command = new CreatePost()
            {
                Content = dto.PostContent,
                BlogName = blogId,
                Title = dto.PostTitle,
                AuthorEmail = _userDataProvider.Email()
            };

            var output = await _mediator.Send(command);

            return Created($"/api/blogs/{blogId}/posts/{output}",output);
        }

        [AllowAnonymous]
        [SwaggerOperation("Gets post details.")]
        [SwaggerResponse(200, "User provided correct post data.",typeof(PostDto))]
        [SwaggerResponse(204, "User provided correct post data.")]
        [SwaggerResponse(404, "This user, blog or post does not exsist.", typeof(ResponseDetails))]
        [SwaggerResponse(400, "User didin't provided value in one of the fields, or provided incorrect value.", typeof(ValidationProblemDetails))]
        [HttpPost("/api/blogs/posts/{postId}")]
        public async Task<ActionResult> Get([FromRoute] Guid postId)
        {
            var query = new GetPost()
            {
                PostId = postId
            };

            var post = await _mediator.Send(query);

            if (post == null)
                return NoContent();

            var output = new PostDto(post);

            return Ok(output);
        }

        [SwaggerOperation("Removes post.")]
        [SwaggerResponse(200, "Post has been removed.", typeof(PostDto))]
        [SwaggerResponse(400, "Post doesnt belong to logged user.", typeof(ResponseDetails))]
        [SwaggerResponse(404, "This post does not exsist.", typeof(ResponseDetails))]
        [SwaggerResponse(400, "User didin't provided value in one of the fields, or provided incorrect value.", typeof(ValidationProblemDetails))]
        [HttpDelete("/api/blogs/posts/{postId}")]
        public async Task<ActionResult> Delete([FromRoute] Guid postId)
        {
            var query = new DeletePost()
            {
                PostId = postId
            };

            await _mediator.Send(query);

            return Ok();
        }
    }
}
