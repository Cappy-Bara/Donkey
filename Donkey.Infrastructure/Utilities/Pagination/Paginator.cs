using Donkey.Core.Exceptions;
using Donkey.Core.Utilities.Pagination;
using Donkey.Infrastructure.Extensions.EF;
using System.Linq.Expressions;

namespace Donkey.Infrastructure.Utilities.Pagination
{
    public class Paginator<T>
    {
        internal IQueryable<T> _collection;
        private readonly int _pageSize;

        public Paginator(IQueryable<T> collection, int pageSize)
        {
            _collection = collection;
            _pageSize = pageSize;
        }

        public async Task<PaginatedResult<T>> GetElementsFromPage(int pageNumber)
        {
            if (_collection is null)
            {
                if(pageNumber == 1)
                    return PaginatedResult<T>.Empty;
                return PaginatedResult<T>.Invalid;
            }
               

            int pageCount = (int)Math.Ceiling(((double)_collection.Count() / (double)_pageSize));

            if (pageNumber > pageCount)
            {
                if(pageNumber > 1)
                    return PaginatedResult<T>.Invalid;

                return PaginatedResult<T>.Empty;
            }

            var items = await _collection.Skip(_pageSize * (pageNumber - 1)).Take(_pageSize).ToListAsyncSafe();
            
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
        public static Paginator<T> OrderBy<T,TKey>(this Paginator<T> paginator, Expression<Func<T, TKey>> predicate)
        {
            paginator._collection = paginator._collection.OrderBy(predicate);
            return paginator;
        }
    }
}
