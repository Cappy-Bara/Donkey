using Donkey.API.DTOs.Requests;
using Donkey.API.DTOs.Responses;
using Donkey.API.Settings.Authentication;
using Donkey.Core.Actions.Queries.Account.Login;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Donkey.API.Controllers
{
    [AllowAnonymous]
    public class AccountsController : BaseController
    {
        private readonly AuthenticationSettings _authSettings;

        public AccountsController(IMediator mediator, AuthenticationSettings authSettings) : base(mediator)
        {
            _authSettings = authSettings;
        }

        [SwaggerOperation("Logs user in aplication")]
        [SwaggerResponse(200,"User provided correct email and password",typeof(UserDataDto))]
        [SwaggerResponse(404,"User provided incorrect login",typeof(ResponseDetails))]
        [SwaggerResponse(400,"User provided correct login and incorrect password", typeof(ValidationProblemDetails))]
        [SwaggerResponse(400,"User didin't provided value in one of the fields, or provided incorrect value.",typeof(ValidationProblemDetails))]
        [HttpPost("login")]
        public async Task<ActionResult<UserDataDto>> Login([FromBody]LoginDto loginData)
        {
            var query = new Login
            {
                Email = loginData.Email,
                Password = loginData.Password,
                JwtKey = _authSettings.JwtKey,
                JwtIssuer = _authSettings.JwtIssuer,
                ExpireDays = _authSettings.JwtExpireDays,
            };

            var token = await _mediator.Send(query);

            var output = new UserDataDto() 
            {
                Token = token
            };

            return Ok(output);
        }
    }
}
