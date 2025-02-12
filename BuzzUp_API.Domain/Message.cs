using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Message : Entity
    {
        public string Content { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public bool IsRead { get; set; }
    }
}
