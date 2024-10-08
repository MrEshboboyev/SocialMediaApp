﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.UI.ViewModels;
using System.Security.Claims;

namespace SocialMediaApp.UI.Controllers
{
    [Authorize]
    public class PostController(IPostService postService,
        ICommentService commentService) : Controller
    {
        private readonly IPostService _postService = postService;
        private readonly ICommentService _commentService = commentService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required;");
        #endregion

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();  // Fetch posts

            // Set IsLikedByCurrentUser for each post
            foreach (var post in posts.Data)
            {
                post.IsLikedByCurrentUser = post.Likes.Any(like => like.UserId == GetUserId());
            }

            return View(posts.Data);
        }

        public async Task<IActionResult> UserIndex()
        {
            var userPosts = await _postService.GetUserPostsAsync(GetUserId());
            // Set IsLikedByCurrentUser for each post
            foreach (var post in userPosts.Data)
            {
                post.IsLikedByCurrentUser = post.Likes.Any(like => like.UserId == GetUserId());
            }

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

        // Details with comments
        [HttpGet]
        public async Task<IActionResult> Details(int postId)
        {
            var post = await _postService.GetPostAsync(postId);
            var postComments = await _commentService.GetPostCommentsAsync(postId);

            PostDetailsVM model = new()
            {
                Comments = postComments.Data,
                Post = post.Data
            };

            return View(model);
        }
    }
}
