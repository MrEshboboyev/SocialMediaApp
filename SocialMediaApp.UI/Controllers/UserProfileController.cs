using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.UI.ViewModels;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class UserProfileController(IUserProfileService userProfileService) : 
        Controller
    {
        private readonly IUserProfileService _userProfileService = userProfileService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required;");
        #endregion

        public async Task<IActionResult> Index()
        {
            var userProfile = (await _userProfileService.GetProfileAsync(GetUserId())).Data;

            UserProfileVM profileVM = new()
            {
                Bio = userProfile.Bio,
                DateOfBirth = userProfile.DateOfBirth,
                Email = User.FindFirstValue(ClaimTypes.Email),
                ProfilePictureUrl = userProfile.ProfilePictureUrl,
                RecentPosts = null,
                UserName = User.FindFirstValue(ClaimTypes.Email),
                Website = userProfile.Website
            };

            return View(profileVM);
        }
    }
}
