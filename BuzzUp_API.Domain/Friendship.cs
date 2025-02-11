using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Friendship : Entity
    {
        public int FriendRequestStatusId { get; set; }
        public virtual FriendRequestStatus FriendRequestStatus { get; set; }
        public virtual ICollection<UserFriendship> Users { get; set; } = new HashSet<UserFriendship>();
    }
}
