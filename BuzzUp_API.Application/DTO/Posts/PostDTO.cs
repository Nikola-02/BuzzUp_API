using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Posts
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int VisibilityTypeId { get; set; }
        public string VisibilityName { get; set; }
        public int? FeelingTypeId { get; set; }
        public string FeelingName { get; set; }
        public string FeelingIcon { get; set; }
        public IEnumerable<string> Images { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
