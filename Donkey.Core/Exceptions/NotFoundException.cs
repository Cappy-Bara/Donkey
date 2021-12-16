using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Exceptions
{
    public class NotFoundException : DonkeyException
    {
        public NotFoundException(string message) : base(message,404)
        {

        }
    }
}
