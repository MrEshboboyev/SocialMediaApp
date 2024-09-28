using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs
{
    public class PostUpdateDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string? UserId { get; set; }
        public string? OwnerName { get; set; }
    }
}
