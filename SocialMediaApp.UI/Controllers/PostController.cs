using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required;");
        #endregion

        public async Task<IActionResult> Index()
        {
            var allPosts = await _postService.GetAllPostsAsync();
            return View(allPosts.Data);
        }

        public async Task<IActionResult> UserIndex()
        {
            var userPosts = await _postService.GetUserPostsAsync(GetUserId());
            return View(userPosts.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PostDTO postDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            postDTO.UserId = GetUserId();
            var result = await _postService.CreatePostAsync(postDTO);

            if (result.Success)
            {
                TempData["success"] = "Post created successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["error"] = result.Message;
            return View(postDTO);
        }

        public async Task<IActionResult> Update(int postId)
        {
            var post = await _postService.GetPostAsync(postId);
            return View(post.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostDTO postDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PostUpdateDTO postUpdateDTO = new()
            {
                UserId = GetUserId(),
                Id = postDTO.Id,
                Content = postDTO.Content
            };

            var result = await _postService.UpdatePostAsync(postUpdateDTO);

            if (result.Success)
            {
                TempData["success"] = "Post updated successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["error"] = result.Message;
            return View(postUpdateDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int postId)
        {
            PostDTO postDTO = new()
            {
                Id = postId,
                UserId = GetUserId()
            };
            var result = await _postService.DeletePostAsync(postDTO);

            if (result.Success)
            {
                TempData["success"] = "Post deleted successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["error"] = result.Message;
            return RedirectToAction(nameof(UserIndex));
        }
    }
}
