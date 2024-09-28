using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Application.Common.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must be match.")]
        public string ConfirmPassword { get; set; }

        // User Profile fields
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ProfilePictureUrl { get; set; } // URL of the profile picture
        public string Website { get; set; }        // User's personal website or blog
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }  // Optional date of birth
    }
}
