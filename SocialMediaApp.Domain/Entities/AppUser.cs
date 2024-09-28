using Microsoft.AspNetCore.Identity;

namespace SocialMediaApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public UserProfile Profile { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
