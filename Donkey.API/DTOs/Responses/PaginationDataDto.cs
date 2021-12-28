using Donkey.Core.Aggregates;

namespace Donkey.API.DTOs.Responses
{
    public class PaginationDataDto
    {
        public int AvailablePages { get; set; }
        public int FirstElementIndex { get; set; }
        public int LastElementIndex { get; set; }

        public PaginationDataDto(IPaginated data)
        {
            AvailablePages = data.AvailablePages;
            FirstElementIndex = data.FirstElementIndex;
            LastElementIndex = data.LastElementIndex;
        }
    }
}
