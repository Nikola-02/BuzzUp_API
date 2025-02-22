﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class FriendRequestStatus : NamedEntity
    {
        public virtual ICollection<Friendship> Friendships { get; set; } = new HashSet<Friendship>();
    }
}
