using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class NotificationType : NamedEntity
    {
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
