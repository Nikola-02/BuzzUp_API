using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Users
{
    public class UserMiniDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string Workplace { get; set; }
        public string University { get; set; }
        public string Website { get; set; }
        public string Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int PostCount { get; set; }
        public int FriendCount { get; set; }
        public string FriendshipStatus { get; set; }
    }
}
