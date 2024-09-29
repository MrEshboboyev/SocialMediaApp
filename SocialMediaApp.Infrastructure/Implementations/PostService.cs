using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Implementations
{
    public class PostService(IUnitOfWork unitOfWork, IMapper mapper) :
        IPostService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;   
        private readonly IMapper _mapper = mapper;   


        public async Task<ResponseDTO<IEnumerable<PostDTO>>> GetAllPostsAsync()
        {
            try
            {
                var allPosts = await _unitOfWork.Post.GetAllAsync(
                    includeProperties: "User,Likes");

                var mappedPosts = _mapper.Map<IEnumerable<PostDTO>>(allPosts);

                return new ResponseDTO<IEnumerable<PostDTO>>(mappedPosts);
            }
            catch (Exception ex) 
            {
                return new ResponseDTO<IEnumerable<PostDTO>>(ex.Message);
            }
        }

        public async Task<ResponseDTO<IEnumerable<PostDTO>>> GetUserPostsAsync(string userId)
        {
            try
            {
                var allPosts = await _unitOfWork.Post.GetAllAsync(
                    filter: p => p.UserId.Equals(userId),
                    includeProperties: "User,Likes");

                var mappedPosts = _mapper.Map<IEnumerable<PostDTO>>(allPosts);

                return new ResponseDTO<IEnumerable<PostDTO>>(mappedPosts);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<PostDTO>>(ex.Message);
            }
        }

        public async Task<ResponseDTO<PostDTO>> GetPostAsync(int postId)
        {
            try
            {
                var post = await _unitOfWork.Post.GetAsync(
                    filter: p => p.Id.Equals(postId),
                    includeProperties: "User,Likes");

                var mappedPost = _mapper.Map<PostDTO>(post);

                return new ResponseDTO<PostDTO>(mappedPost);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PostDTO>(ex.Message);
            }
        }



        public async Task<ResponseDTO<bool>> CreatePostAsync(PostDTO postDTO)
        {
            try
            {
                var postForDb = _mapper.Map<Post>(postDTO);
                postDTO.CreatedAt = DateTime.Now;

                await _unitOfWork.Post.AddAsync(postForDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> UpdatePostAsync(PostUpdateDTO postUpdateDTO)
        {
            try
            {
                var postFromDb = await _unitOfWork.Post.GetAsync(
                    p => p.UserId.Equals(postUpdateDTO.UserId) && 
                    p.Id.Equals(postUpdateDTO.Id)
                    ) ?? throw new Exception("Post not found!");

                // mapped fields
                _mapper.Map(postUpdateDTO, postFromDb);

                await _unitOfWork.Post.UpdateAsync(postFromDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> DeletePostAsync(PostDTO postDTO)
        {
            try
            {
                var postFromDb = await _unitOfWork.Post.GetAsync(
                    p => p.UserId.Equals(postDTO.UserId) &&
                    p.Id.Equals(postDTO.Id)
                    ) ?? throw new Exception("Post not found!");

                await _unitOfWork.Post.RemoveAsync(postFromDb);
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
