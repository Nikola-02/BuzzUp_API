using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Workplace { get; set; }
        public string University { get; set; }
        public bool IsOnline { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<UserFriendship> Friendships { get; set; } = new HashSet<UserFriendship>();
    }
}
