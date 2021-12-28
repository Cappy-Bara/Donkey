using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Aggregates
{
    public interface IPaginated
    {
        public int AvailablePages { get; set; }
        public int FirstElementIndex { get; set; }
        public int LastElementIndex { get; set; }
    }
}
