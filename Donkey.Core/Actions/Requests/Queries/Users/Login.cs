using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Requests.Queries.Users
{
    public class Login : IRequest<List<Claim>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
