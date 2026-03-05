using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Users
{
    public class UserUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Workplace { get; set; }
        public string University { get; set; }
        public string Website { get; set; }
        public string Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
