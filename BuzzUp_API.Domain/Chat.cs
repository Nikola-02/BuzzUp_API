using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Chat : Entity
    {
        public virtual ICollection<UserChat> UserChats { get; set; } = new HashSet<UserChat>();
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
