using BuzzUp_API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Posts
{
    public class PostSearch : TablesSearch
    {
        public int? UserId { get; set; }
    }
}
