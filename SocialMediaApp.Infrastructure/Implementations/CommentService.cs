using AutoMapper;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Implementations
{
    public class CommentService(IUnitOfWork unitOfWork, IMapper mapper) :
        ICommentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<ResponseDTO<IEnumerable<CommentDTO>>> GetPostCommentsAsync(int postId)
        {
            try
            {
                var postComments = await _unitOfWork.Comment.GetAllAsync(
                    filter: c => c.PostId.Equals(postId),
                    includeProperties: "User"
                    );

                var mappedComments = _mapper.Map<IEnumerable<CommentDTO>>(postComments);

                return new ResponseDTO<IEnumerable<CommentDTO>>(mappedComments);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CommentDTO>>(ex.Message);
            }
        }

        public async Task<ResponseDTO<CommentDTO>> GetCommentAsync(int commentId)
        {
            try
            {
                var comment = await _unitOfWork.Comment.GetAllAsync(
                    filter: c => c.Id.Equals(commentId),
                    includeProperties: "User"
                    );

                var mappedComment = _mapper.Map<CommentDTO>(comment);

                return new ResponseDTO<CommentDTO>(mappedComment);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CommentDTO>(ex.Message);
            }
        }


        public async Task<ResponseDTO<bool>> CreateCommentAsync(CommentDTO commentDTO)
        {
            try
            {
                var commentForDb = _mapper.Map<Comment>(commentDTO);
                commentForDb.CreatedAt = DateTime.Now;

                await _unitOfWork.Comment.AddAsync(commentForDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> UpdateCommentAsync(CommentUpdateDTO commentUpdateDTO)
        {
            try
            {
                var commentFromDb = await _unitOfWork.Comment.GetAsync(
                    c => c.Id.Equals(commentUpdateDTO.Id) && 
                    c.PostId.Equals(commentUpdateDTO.PostId) &&
                    c.UserId.Equals(commentUpdateDTO.UserId)
                    ) ?? throw new Exception("Comment not found!");

                // mapping fields
                _mapper.Map(commentUpdateDTO, commentFromDb);

                await _unitOfWork.Comment.UpdateAsync(commentFromDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> DeleteCommentAsync(CommentDTO commentDTO)
        {
            try
            {
                var commentFromDb = await _unitOfWork.Comment.GetAsync(
                    c => c.Id.Equals(commentDTO.Id) &&
                    c.PostId.Equals(commentDTO.PostId) &&
                    c.UserId.Equals(commentDTO.UserId)
                    ) ?? throw new Exception("Comment not found!");

                await _unitOfWork.Comment.RemoveAsync(commentFromDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }
    }
}
