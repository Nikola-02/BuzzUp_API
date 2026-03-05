using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Role : NamedEntity
    {
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
