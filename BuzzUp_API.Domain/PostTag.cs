using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class PostTag : CompositeEntity
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
