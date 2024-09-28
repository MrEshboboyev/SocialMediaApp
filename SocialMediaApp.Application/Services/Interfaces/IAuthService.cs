using SocialMediaApp.Application.Common.Models;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<string>> LoginAsync(LoginModel loginModel);
        Task<ResponseDTO<string>> RegisterAsync(RegisterModel registerModel);
        Task<ResponseDTO<string>> GenerateJwtToken(AppUser user, IEnumerable<string> roles);
    }
}
