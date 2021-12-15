using Donkey.API.ClientDataProviders;
using Donkey.API.Settings.Authentication;
using Donkey.Core.Actions.Requests.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly AuthenticationSettings _authSettings;

        public UsersController(IMediator mediator, AuthenticationSettings authSettings) : base(mediator)
        {
            _authSettings = authSettings;
            _authSettings = authSettings;
        }

        [HttpPut]
        public async Task<ActionResult> Login([FromBody]Login loginData)
        {
            var claims = await _mediator.Send(loginData);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authSettings.JwtIssuer, _authSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var output = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(output);
        }
    }
}
