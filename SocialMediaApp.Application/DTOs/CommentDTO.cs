using SocialMediaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }                
        public string Content { get; set; }        
        public DateTime CreatedAt { get; set; }    

        // Foreign Keys
        public string UserId { get; set; }           
        public int PostId { get; set; }            

        // Navigation Properties
        public string? OwnerName { get; set; }             
    }
}
