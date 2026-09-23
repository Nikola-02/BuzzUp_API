using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Friendship : Entity
    {
        public int SenderUserId { get; set; }
        public virtual User Sender { get; set; }

        public int ReceiverUserId { get; set; }
        public virtual User Receiver { get; set; }

        public int FriendRequestStatusId { get; set; }
        public virtual FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
