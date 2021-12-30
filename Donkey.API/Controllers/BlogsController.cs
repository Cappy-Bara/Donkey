using Donkey.API.ClientDataProviders;
using Donkey.API.DTOs.Requests;
using Donkey.API.DTOs.Responses;
using Donkey.Core.Actions.Commands.Blogs;
using Donkey.Core.Actions.Commands.Blogs.Delete;
using Donkey.Core.Actions.Queries.Blogs.GetBlogs;
using Donkey.Core.Entities;
using Donkey.Core.Utilities.Pagination;
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
        [SwaggerResponse(201, "User provided correct blog creation data")]
        [SwaggerResponse(404, "This user does not exsist", typeof(ResponseDetails))]
        [SwaggerResponse(401, "User is unauthorized", typeof(ResponseDetails))]
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

            return Created($"/api/blogs/{blogData.Name}",blogData.Name);
        }

        [SwaggerOperation("Returns all blogs of logged user.")]
        [SwaggerResponse(200, "User exists and has some blogs",typeof(PaginatedDto<BlogDto>))]
        [SwaggerResponse(204, "User exist but didn't have any blogs")]
        [SwaggerResponse(404, "This user does not exsist", typeof(ResponseDetails))]
        [SwaggerResponse(401, "User is unauthorized")]
        [HttpGet]
        public async Task<ActionResult<PaginatedDto<BlogDto>>> GetAll([FromQuery]PaginationDto dto)
        {
            var command = new GetBlogs()
            {
                Email = _userDataProvider.Email(),
                PageNumber = dto.Page,
                PostsOnPageAmount = dto.Limit,
            };

            var blogs = await _mediator.Send(command);

            if (blogs == PaginatedResult<Blog>.Empty)
                return NoContent();

            var output = new PaginatedDto<BlogDto>()
            {
                Items = blogs.Items.Select(x => new BlogDto() {Name = x.Name }).ToList(),
                PaginationData = new PaginationDataDto(blogs),
            };

            return Ok(output);
        }

        [SwaggerOperation("Removes user's blog.")]
        [SwaggerResponse(200, "Blog has been removed successfully.", typeof(BlogsDto))]
        [SwaggerResponse(404, "User or blog does not exsist", typeof(ResponseDetails))]
        [SwaggerResponse(400, "Blog doesn't belong to currently logged user.", typeof(ResponseDetails))]
        [SwaggerResponse(401, "User is unauthorized")]
        [HttpDelete("{blogName}")]
        public async Task<ActionResult> Delete([FromRoute]string blogName)
        {
            var command = new DeleteBlog()
            {
                BlogName = blogName,
                Email = _userDataProvider.Email(),
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
