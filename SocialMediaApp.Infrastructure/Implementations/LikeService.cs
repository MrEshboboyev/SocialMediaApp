﻿using AutoMapper;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.Domain.Entities;
using System.Data.SqlTypes;

namespace SocialMediaApp.Infrastructure.Implementations
{
    public class LikeService(IUnitOfWork unitOfWork,
        IMapper mapper) : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDTO<bool>> LikePostAsync(LikeDTO likeDTO)
        {
            try
            {
                var likeForDb = _mapper.Map<Like>(likeDTO);

                await _unitOfWork.Like.AddAsync(likeForDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> UnlikePostAsync(LikeDTO likeDTO)
        {
            try
            {
                // get this like
                var likeFromDb = await _unitOfWork.Like.GetAsync(
                    l => l.PostId.Equals(likeDTO.PostId) &&
                    l.UserId.Equals(likeDTO.UserId)
                    ) ?? throw new Exception("Like not found!");

                await _unitOfWork.Like.RemoveAsync(likeFromDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> HasUserLikedPostAsync(LikeDTO likeDTO)
        {
            try
            {
                var likeFromDb = await _unitOfWork.Like.GetAsync(
                    l => l.PostId.Equals(likeDTO.PostId) &&
                    l.UserId.Equals(likeDTO.UserId)
                    );

                if (likeFromDb is null)
                    return new ResponseDTO<bool>(false);

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }

        public async Task<ResponseDTO<int>> GetPostLikeCountAsync(int postId)
        {
            try
            {
                var postLikesFromDb = await _unitOfWork.Like.GetAllAsync(
                    l => l.PostId.Equals(postId)
                    );

                return new ResponseDTO<int>(postLikesFromDb.Count());
            }
            catch (Exception ex)
            {
                return new ResponseDTO<int>(ex.Message);
            }
        }

        public async Task<ResponseDTO<ToggleLikeResultDTO>> ToggleLikeAsync(int postId, string userId)
        {
            try
            {
                // Step 1: Check if the user has already liked the post
                var hasLikedResponse = await HasUserLikedPostAsync(new LikeDTO
                {
                    PostId = postId,
                    UserId = userId
                });

                if (!hasLikedResponse.Success)
                    throw new Exception("Error checking like status.");

                bool isLiked = hasLikedResponse.Data;

                // Step 2: Like or Unlike the post based on the current status
                if (isLiked)
                {
                    // Unlike the post
                    var unlikeResponse = await UnlikePostAsync(new LikeDTO
                    {
                        PostId = postId,
                        UserId = userId
                    });

                    if (!unlikeResponse.Success)
                        throw new Exception("Error unliking post.");

                    isLiked = false;
                }
                else
                {
                    // Like the post
                    var likeResponse = await LikePostAsync(new LikeDTO
                    {
                        PostId = postId,
                        UserId = userId
                    });

                    if (!likeResponse.Success)
                        throw new Exception("Error liking post.");

                    isLiked = true;
                }

                // Step 3: Get the updated like count
                var likeCountResponse = await GetPostLikeCountAsync(postId);

                if (!likeCountResponse.Success)
                    throw new Exception("Error getting like count.");

                // Return the updated like status and like count
                return new ResponseDTO<ToggleLikeResultDTO>(new ToggleLikeResultDTO
                {
                    IsLiked = isLiked,
                    LikeCount = likeCountResponse.Data
                });
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ToggleLikeResultDTO>(ex.Message);
            }
        }
    }
}
