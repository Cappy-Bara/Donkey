using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.ClientDataProviders
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserDataProvider(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string Email()
        {
            return _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
    }
}
