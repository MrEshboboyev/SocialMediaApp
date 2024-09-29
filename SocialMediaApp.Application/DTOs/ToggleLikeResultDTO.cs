using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.DTOs;

public class ToggleLikeResultDTO
{
    public bool IsLiked { get; set; }  // Whether the post is now liked by the current user
    public int LikeCount { get; set; } // The updated number of likes on the post
}