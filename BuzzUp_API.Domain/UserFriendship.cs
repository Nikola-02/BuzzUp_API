using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class UserFriendship : CompositeEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int FriendshipId { get; set; }
        public virtual Friendship Friendship { get; set; }
    }
}
