using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Notification : Entity
    {
        public int RecipientUserId { get; set; }
        public virtual User Recipient { get; set; }

        public int ActorUserId { get; set; }
        public virtual User Actor { get; set; }

        public int NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }

        public int? ReactionTypeId { get; set; }
        public virtual ReactionType ReactionType { get; set; }

        public bool IsRead { get; set; }
    }
}
