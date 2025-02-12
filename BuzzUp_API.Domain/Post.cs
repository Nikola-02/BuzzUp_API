using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int VisibilityTypeId { get; set; }
        public virtual VisibilityType VisibilityType { get; set; }
        public int FeelingTypeId { get; set; }
        public virtual FeelingType FeelingType { get; set; }
        public virtual ICollection<PostMedia> PostMedias { get; set; } = new HashSet<PostMedia>();
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<User> SavedByUsers { get; set; } = new HashSet<User>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
