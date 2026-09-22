using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string reason) : base(reason)
        {

        }
    }
}
