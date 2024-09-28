namespace SocialMediaApp.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }                // Primary Key
        public string Content { get; set; }        // Post content (could be text, URL to image/video)
        public DateTime CreatedAt { get; set; }    // Date when post was created

        // Foreign Key
        public int UserId { get; set; }            // Foreign key to User
        public AppUser User { get; set; }             // Navigation property to User

        // Navigation Properties
        public ICollection<Comment> Comments { get; set; } // One-to-Many relationship with Comment
        public ICollection<Like> Likes { get; set; }       // One-to-Many relationship with Like
    }
}
