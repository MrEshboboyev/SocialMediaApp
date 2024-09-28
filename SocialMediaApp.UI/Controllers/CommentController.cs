using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class CommentController(ICommentService commentService) : Controller
    {
        private readonly ICommentService _commentService = commentService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required;");
        #endregion

        [HttpPost]
        public async Task<IActionResult> Create(CommentDTO commentDTO)
        {
            commentDTO.UserId = GetUserId();

            var result = await _commentService.CreateCommentAsync(commentDTO);

            if (result.Success)
            {
                TempData["success"] = "Comment successfully created!";
                return RedirectToAction("Details", "Post", new {commentDTO.PostId});
            }

            TempData["error"] = $"Failed to create comment process. Error : {result.Message}";
            return RedirectToAction("Details", "Post", new { commentDTO.PostId });
        }
    }
}
