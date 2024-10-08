﻿using SocialMediaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }               
        public string Content { get; set; }       
        public DateTime CreatedAt { get; set; }    

        public string? UserId { get; set; }           
        public string? OwnerName { get; set; }

        public List<LikeDTO> Likes { get; set; } = [];

        // New property to track if the current user has liked this post
        public bool IsLikedByCurrentUser { get; set; }
    }
}
