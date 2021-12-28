using Donkey.Core.Utilities.Pagination;
using Donkey.Infrastructure.Utilities.Pagination;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Donkey.Tests.Core.Utilities
{
    public class PaginatorTests
    {
        [Theory]
        [InlineData(1,1,5)]
        [InlineData(2,6,10)]
        public async Task Paginator_ElementsDividedWithoutRest_ShouldReturnRightElements(int pageNumber, int firstElementIndex,int lastElementIndex)
        {
            IQueryable<int> values = new List<int>() {1,2,3,4,5,6,7,8,9,10}.AsQueryable();

            var paginator = new Paginator<int>(values,5);

            var sus = await paginator.GetElementsFromPage(pageNumber);

            sus.LastElementIndex.Should().Be(lastElementIndex);
            sus.FirstElementIndex.Should().Be(firstElementIndex);
            sus.AvailablePages.Should().Be(2);
            sus.Items.Count().Should().Be(5);
        }

        [Theory]
        [InlineData(1, 1, 3, 3)]
        [InlineData(2, 4, 6, 3)]
        [InlineData(3, 7, 9, 3)]
        [InlineData(4, 10, 10, 1)]
        public async Task Paginator_ElementsDividedWithRest_ShouldReturnRightElementList(int pageNumber, int firstElementIndex, int lastElementIndex,int elementsOnPage)
        {
            IQueryable<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.AsQueryable();

            var paginator = new Paginator<int>(values, 3);

            var sus = await paginator.GetElementsFromPage(pageNumber);

            sus.LastElementIndex.Should().Be(lastElementIndex);
            sus.FirstElementIndex.Should().Be(firstElementIndex);
            sus.AvailablePages.Should().Be(4);
            sus.Items.Count().Should().Be(elementsOnPage);
        }

        [Fact]
        public async Task Paginator_WantNotExistingPage_ShouldReturnInvalidResult()
        {
            IQueryable<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.AsQueryable();

            var paginator = new Paginator<int>(values, 3);

            var sus = await paginator.GetElementsFromPage(8);

            sus.Should().Be(PaginatedResult<int>.Invalid);
        }

        [Fact]
        public async Task Paginator_NoElementsToPaginateAndWantFirstPage_ShouldReturnEmptyResult()
        {
            IQueryable<int> values = new List<int>().AsQueryable();

            var paginator = new Paginator<int>(values, 3);

            var sus = await paginator.GetElementsFromPage(1);

            sus.Should().Be(PaginatedResult<int>.Empty);
        }

        [Fact]
        public async Task Paginator_NullElementToPaginateAndWantFirstPage_ShouldReturnEmptyResult()
        {
            IQueryable<int> values = null;

            var paginator = new Paginator<int>(values, 3);

            var sus = await paginator.GetElementsFromPage(1);

            sus.Should().Be(PaginatedResult<int>.Empty);
        }
        
        [Fact]
        public async Task Paginator_NullElementToPaginateAndWantNotExistingPage_ShouldReturnEmptyResult()
        {
            IQueryable<int> values = null;

            var paginator = new Paginator<int>(values, 3);

            var sus = await paginator.GetElementsFromPage(5);

            sus.Should().Be(PaginatedResult<int>.Invalid);
        }

        [Theory]
        [InlineData(1,"first","second","third")]
        [InlineData(2,"fourth","fifth","sixth")]
        public async Task Paginator_OrderedByValue_ShouldReturnRightElementList(int pageNumber, string firstElement,string secondElement = null,string thirdElement = null)
        {
            IQueryable<(string,int)> values = new List<(string, int)>() { ("fourth",4),("second",2),("fifth",5),("sixth",6),("seventh",7),("first",1),("third",3)}.AsQueryable();

            var paginator = new Paginator<(string,int)>(values, 3);
            paginator.OrderBy(x => x.Item2);
            var sus = await paginator.GetElementsFromPage(pageNumber);

            sus.Items.Select(x => x.Item1).Should().ContainInOrder(firstElement,secondElement,thirdElement);
        }
    }
}
