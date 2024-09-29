using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.Services.Interfaces;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class LikeController(ILikeService likeService) : Controller
    {
        private readonly ILikeService _likeService = likeService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required;");
        #endregion

        [HttpPost]
        public async Task<IActionResult> ToggleLike(int postId)
        {
            var currentUserId = GetUserId();  // Get the currently logged-in user ID

            // Toggle the like (like or unlike depending on the current state)
            var result = await _likeService.ToggleLikeAsync(postId, currentUserId);

            if (result.Success)
            {
                return Json(new
                {
                    success = true,
                    isLiked = result.Data.IsLiked,
                    likeCount = result.Data.LikeCount
                });
            }

            return Json(new
            {
                success = false,
                message = result.Message
            });
        }
    }
}
