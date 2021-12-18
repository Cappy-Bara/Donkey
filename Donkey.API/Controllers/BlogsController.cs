using Donkey.API.ClientDataProviders;
using Donkey.API.DTOs.Requests;
using Donkey.Core.Actions.Commands.Blogs;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Donkey.API.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IUserDataProvider _userDataProvider;

        public BlogsController(IMediator mediator, IUserDataProvider userDataProvider) : base(mediator)
        {
            _userDataProvider = userDataProvider;
        }

        [SwaggerOperation("Creates new blog for logged user")]
        [SwaggerResponse(200, "User provided correct blog creation data")]
        [SwaggerResponse(404, "This user does not exsist", typeof(ResponseDetails))]
        [SwaggerResponse(400, "User didin't provided value in one of the fields, or provided incorrect value.", typeof(ValidationProblemDetails))]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBlogDto blogData)
        {
            var command = new CreateBlog()
            {
                UserEmail = _userDataProvider.Email(),
                BlogName = blogData.Name
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}
