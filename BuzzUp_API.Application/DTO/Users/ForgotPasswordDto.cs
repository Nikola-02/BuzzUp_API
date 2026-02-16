using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Users
{
    public class ForgotPasswordDto
    {
        public string UserEmail { get; set; }
        public string SmtpEmail { get; set; }
        public string SmtpPassword { get; set; }
        public string ResetPasswordUrl { get; set; }
    }
}
