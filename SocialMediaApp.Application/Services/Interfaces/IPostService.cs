using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResponseDTO<IEnumerable<PostDTO>>> GetAllPostsAsync();
        Task<ResponseDTO<IEnumerable<PostDTO>>> GetUserPostsAsync(string userId);
        Task<ResponseDTO<PostDTO>> GetPostAsync(int postId);
        Task<ResponseDTO<bool>> CreatePostAsync(PostDTO postDTO);
        Task<ResponseDTO<bool>> UpdatePostAsync(PostUpdateDTO postUpdateDTO);
        Task<ResponseDTO<bool>> DeletePostAsync(PostDTO postDTO);
    }
}
