using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Exceptions
{
    public class BadRequestException : DonkeyException
    {
        public BadRequestException(string message) : base(message,400)
        {
        }
    }
}
