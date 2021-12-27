using Donkey.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Utilities.Pagination
{
    public class Paginator<T>
    {
        internal IEnumerable<T> _collection;
        private readonly int _pageSize;

        public Paginator(IEnumerable<T> collection, int pageSize)
        {
            _collection = collection;
            _pageSize = pageSize;
        }

        public PaginatedResult<T> GetElementsFromPage(int pageNumber)
        {
            int pageCount = (int)Math.Ceiling(((double)_collection.Count() / (double)_pageSize));

            if (pageNumber > pageCount)
                return PaginatedResult<T>.Empty;

            var items = _collection.Skip(_pageSize * (pageNumber - 1)).Take(_pageSize);
            int firstElementIndex = (pageNumber - 1) * _pageSize + 1;
            int lastElementIndex = firstElementIndex + items.Count() - 1;

            var output = new PaginatedResult<T>()
            {
                Items = items,
                AvailablePages = pageCount,
                FirstElementIndex = firstElementIndex,
                LastElementIndex = lastElementIndex
            };

            return output;
        }
    }

    public static class PaginatorExtensions
    {
        public static Paginator<T> OrderBy<T,TKey>(this Paginator<T> paginator, Func<T, TKey> predicate)
        {
            paginator._collection = paginator._collection.OrderBy(predicate);
            return paginator;
        }
    }
}
