using BuzzUp_API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.DTO.Posts
{
    public class PostUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int VisibilityTypeId { get; set; }
        public int? FeelingTypeId { get; set; }
        public string Image { get; set; }
    }
}
