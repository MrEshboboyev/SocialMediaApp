using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<ResponseDTO<UserProfileDTO>> GetProfile(string userId);
        Task<ResponseDTO<bool>> UpdateProfile(UserProfileDTO userProfileDTO);
    }
}
