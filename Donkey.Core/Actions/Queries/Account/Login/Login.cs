using MediatR;

namespace Donkey.Core.Actions.Queries.Account.Login
{
    public class Login : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string JwtKey { get; set; }
        public int ExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
