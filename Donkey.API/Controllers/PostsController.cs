using Donkey.API.ClientDataProviders;
using Donkey.API.DTOs.Requests;
using Donkey.Core.Actions.Commands.Posts;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [SwaggerResponse(404, "This user does not exsist.", typeof(ResponseDetails))]
        [SwaggerResponse(404, "This blog does not exsist.", typeof(ResponseDetails))]
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



    }
}
