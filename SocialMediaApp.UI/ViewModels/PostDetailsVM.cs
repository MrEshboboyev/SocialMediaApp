using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.UI.ViewModels
{
    public class PostDetailsVM
    {
        public PostDTO Post { get; set; }  // Post details
        public IEnumerable<CommentDTO>? Comments { get; set; }  // Post comments
    }
}
