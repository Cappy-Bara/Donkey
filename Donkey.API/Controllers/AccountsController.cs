using AutoMapper;
using Donkey.API.ClientDataProviders;
using Donkey.API.DTOs.Requests;
using Donkey.API.DTOs.Responses;
using Donkey.API.Settings.Authentication;
using Donkey.Core.Actions.Queries.Account;
using Donkey.Core.Actions.Queries.Account.Login;
using Donkey.Core.ValueObjects.Accounts;
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
        private readonly IUserDataProvider _userDataProvider;

        public AccountsController(IMediator mediator, IMapper mapper, AuthenticationSettings authSettings, IUserDataProvider userDataProvider) : base(mediator, mapper)
        {
            _authSettings = authSettings;
            _userDataProvider = userDataProvider;
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

        [SwaggerOperation("Returns user basic data")]
        [SwaggerResponse(200, "User is logged", typeof(UserBasicDataDto))]
        [SwaggerResponse(401, "User is unauthorized", typeof(ResponseDetails))]
        [HttpGet]
        public async Task<ActionResult<UserBasicDataDto>> UserBasicData()
        {
            var email = _userDataProvider.Email();

            var query = new GetUserBasicData(email);
            var response = await _mediator.Send(query);

            var output = _mapper.Map<UserBasicData, UserBasicDataDto>(response);  

            return Ok(output);
        }
    }
}
