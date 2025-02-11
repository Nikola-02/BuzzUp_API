using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Tag : NamedEntity
    {
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
