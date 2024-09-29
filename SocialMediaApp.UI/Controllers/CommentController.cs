using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.Domain.Entities;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class CommentController(ICommentService commentService,
        UserManager<AppUser> userManager) : Controller
    {
        private readonly ICommentService _commentService = commentService;
        private readonly UserManager<AppUser> _userManager = userManager;

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

        [HttpGet]
        public async Task<IActionResult> Update(int commentId)
        {
            var comment = (await _commentService.GetCommentAsync(commentId)).Data;
            if (comment == null || comment.UserId != _userManager.GetUserId(User))
            {
                return Unauthorized();
            }
            return PartialView("_EditCommentPartial", comment);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CommentDTO commentDTO)
        {
            if (ModelState.IsValid && commentDTO.UserId == _userManager.GetUserId(User))
            {
                CommentUpdateDTO commentUpdateDTO = new ()
                {
                    UserId = GetUserId(),
                    Content = commentDTO.Content,
                    Id = commentDTO.Id,
                    OwnerName = commentDTO.OwnerName,
                    PostId = commentDTO.PostId
                };

                var result = await _commentService.UpdateCommentAsync(commentUpdateDTO);
                if (result.Success)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = (await _commentService.GetCommentAsync(commentId)).Data;
            if (comment != null && comment.UserId == _userManager.GetUserId(User))
            {
                var result = await _commentService.DeleteCommentAsync(comment);
                if (result.Success)
                {
                    TempData["success"] = "Comment successfully deleted!";
                    return Json(new { success = true });
                }
            }

            TempData["error"] = $"Failed to Comment deleting process.";
            return Json(new { success = false });
        }
    }
}
