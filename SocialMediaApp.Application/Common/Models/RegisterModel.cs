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
    }
}
