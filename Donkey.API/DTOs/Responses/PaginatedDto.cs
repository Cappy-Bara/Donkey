using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.DTOs.Responses
{
    public class PaginatedDto<T>
    {
        public List<T> Items { get; set; }
        public PaginationDataDto PaginationData { get; set; }
    }
}
