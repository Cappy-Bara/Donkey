using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Core.Entities
{
    public class Blog
    {
        public string Name { get; set; }
        public string OwnerEmail { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
