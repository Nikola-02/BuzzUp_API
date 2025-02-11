using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class PostMediaType : NamedEntity
    {
        public virtual ICollection<PostMedia> PostMedias { get; set; } = new HashSet<PostMedia>();
    }
}
