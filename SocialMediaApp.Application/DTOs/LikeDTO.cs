using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs
{
    public class LikeDTO
    {
        public int Id { get; set; }                // Primary Key

        // Foreign Keys
        public string UserId { get; set; }            // Foreign key to User
        public int PostId { get; set; }            // Foreign key to Post
    }
}
