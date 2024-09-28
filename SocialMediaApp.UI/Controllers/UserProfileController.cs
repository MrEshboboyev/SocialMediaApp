using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                return View(userProfileDTO);

            // Handle Profile Picture Upload
            if (userProfileDTO.ProfilePicture != null && userProfileDTO.ProfilePicture.Length > 0)
            {
                // Define folder path where the profile pictures will be saved
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile-pictures");

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Delete the old profile picture if it exists
                if (!string.IsNullOrEmpty(userProfileDTO.ProfilePictureUrl))
                {
                    var oldImagePath = Path.Combine(uploadsFolder, Path.GetFileName(userProfileDTO.ProfilePictureUrl));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Generate a unique filename for the new profile picture
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(userProfileDTO.ProfilePicture.FileName);
                var newImagePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the new profile picture to the server
                using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    await userProfileDTO.ProfilePicture.CopyToAsync(fileStream);
                }

                // Update the ProfilePictureUrl to point to the new file
                userProfileDTO.ProfilePictureUrl = "/images/profile-pictures/" + uniqueFileName;
            }

            // Update the user profile
            var result = await _userProfileService.UpdateProfileAsync(userProfileDTO);

            if (result.Success)
            {
                TempData["success"] = "Your profile has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = $"Failed to update profile. Error: {result.Message}";
            return View(userProfileDTO);
        }

    }
}
