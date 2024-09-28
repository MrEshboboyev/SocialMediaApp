using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }                // Primary Key
        public string Content { get; set; }        // Comment text
        public DateTime CreatedAt { get; set; }    // Date when comment was created

        // Foreign Keys
        public int UserId { get; set; }            // Foreign key to User
        public int PostId { get; set; }            // Foreign key to Post

        // Navigation Properties
        public AppUser User { get; set; }             // Navigation property to User
        public Post Post { get; set; }             // Navigation property to Post
    }
}
