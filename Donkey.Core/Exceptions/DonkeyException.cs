using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Exceptions
{
    public abstract class DonkeyException : Exception
    {
        public int StatusCode { get; set; }

        public DonkeyException(string message, int code) : base(message)
        {
            StatusCode = code;
        }
    }
}
