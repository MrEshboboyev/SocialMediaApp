using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResponseDTO<IEnumerable<PostDTO>>> GetAllPostsAsync();
        Task<ResponseDTO<IEnumerable<PostDTO>>> GetUserPostsAsync(string userId);
        Task<ResponseDTO<bool>> CreatePostAsync(PostDTO postDTO);
        Task<ResponseDTO<bool>> UpdatePostAsync(PostDTO postDTO);
        Task<ResponseDTO<bool>> DeletePostAsync(PostDTO postDTO);
    }
}
