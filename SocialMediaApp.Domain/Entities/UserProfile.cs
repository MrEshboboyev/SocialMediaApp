namespace SocialMediaApp.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }                // Primary Key
    public string Bio { get; set; }            // Short bio of the user
    public string ProfilePictureUrl { get; set; } // URL of the profile picture
    public string Website { get; set; }        // User's personal website or blog
    public DateTime DateOfBirth { get; set; }  // Optional date of birth

    // Foreign Key
    public string UserId { get; set; }            // Foreign key to User
    public AppUser User { get; set; }             // Navigation property to User
}

