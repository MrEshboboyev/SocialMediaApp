using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface ILikeService
    {
        Task<ResponseDTO<bool>> LikePostAsync(LikeDTO likeDTO);
        Task<ResponseDTO<bool>> UnlikePostAsync(LikeDTO likeDTO);
        Task<ResponseDTO<bool>> HasUserLikedPostAsync(LikeDTO likeDTO);
        Task<ResponseDTO<int>> GetPostLikeCountAsync(int postId);
    }
}
