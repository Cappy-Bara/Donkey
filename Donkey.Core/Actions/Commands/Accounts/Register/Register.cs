using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Actions.Commands.Accounts.Register
{
    public class Register : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Register(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
