using SocialMediaApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ResponseDTO<IEnumerable<CommentDTO>>> GetPostCommentsAsync(int postId);
        Task<ResponseDTO<CommentDTO>> GetCommentAsync(int commentId);
        Task<ResponseDTO<bool>> CreateCommentAsync(CommentDTO commentDTO);
        Task<ResponseDTO<bool>> UpdateCommentAsync(CommentUpdateDTO commentUpdateDTO);
        Task<ResponseDTO<bool>> DeleteCommentAsync(CommentDTO commentDTO);
    }
}
