namespace SocialMediaApp.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }                // Primary Key
        public DateTime CreatedAt { get; set; }    // Date when the like was made

        // Foreign Keys
        public int UserId { get; set; }            // Foreign key to User
        public int PostId { get; set; }            // Foreign key to Post

        // Navigation Properties
        public AppUser User { get; set; }             // Navigation property to User
        public Post Post { get; set; }             // Navigation property to Post
    }
}
