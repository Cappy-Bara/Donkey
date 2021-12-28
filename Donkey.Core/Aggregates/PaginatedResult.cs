using Donkey.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Utilities.Pagination
{
    public class PaginatedResult<T> : IPaginated
    {
        public static PaginatedResult<T> Empty = new PaginatedResult<T>();
        public static PaginatedResult<T> Invalid = new PaginatedResult<T>();

        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int AvailablePages { get; set; }
        public int FirstElementIndex { get; set; }
        public int LastElementIndex { get; set; }
    }
}
