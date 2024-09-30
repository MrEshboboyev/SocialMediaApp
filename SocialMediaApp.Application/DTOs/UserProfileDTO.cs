using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }                // Primary Key
        public string? Bio { get; set; }            // Short bio of the user
        public string? ProfilePictureUrl { get; set; } // URL of the profile picture
        public string? Website { get; set; }        // User's personal website or blog
        public DateTime DateOfBirth { get; set; }  // Optional date of birth

        // Foreign Key
        public string UserId { get; set; }            // Foreign key to User

        // Add this for handling image uploads
        public IFormFile? ProfilePicture { get; set; }
    }
}
