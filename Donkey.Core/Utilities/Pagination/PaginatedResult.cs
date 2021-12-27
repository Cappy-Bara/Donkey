using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Utilities.Pagination
{
    public class PaginatedResult<T>
    {
        public static PaginatedResult<T> Empty = new PaginatedResult<T>();

        public IEnumerable<T> Items { get; internal set; } = new List<T>();
        public int AvailablePages { get; internal set; }
        public int FirstElementIndex { get; internal set; }
        public int LastElementIndex { get; internal set; }
    }

    
}
