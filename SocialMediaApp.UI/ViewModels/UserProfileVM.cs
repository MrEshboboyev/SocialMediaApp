namespace SocialMediaApp.UI.ViewModels
{
    public class UserProfileVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public string? Website { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<PostVM> RecentPosts { get; set; } = new List<PostVM>();
    }
}
