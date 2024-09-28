using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SocialMediaApp.Application.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var profile = await _userProfileService.GetProfileAsync(GetUserId());
            return View(profile.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserProfileDTO userProfileDTO)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Update));

            var result = await _userProfileService.UpdateProfileAsync(userProfileDTO);

            if (result.Success)
            {
                TempData["success"] = "Your Profile updated successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = $"Failed to Profile updating process. Error : {result.Message}";
            return View(userProfileDTO);
        }
    }
}
