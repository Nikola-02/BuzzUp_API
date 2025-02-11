using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class PostMedia : Entity
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Path { get; set; }
        public int PostMediaTypeId { get; set; }
        public virtual PostMediaType PostMediaType { get; set; }
    }
}
