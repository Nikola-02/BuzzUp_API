﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Reaction : CompositeEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int ReactionTypeId { get; set; }
        public virtual ReactionType ReactionType { get; set; }
    }
}
