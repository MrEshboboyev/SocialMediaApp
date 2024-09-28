using AutoMapper;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Infrastructure.Implementations
{
    public class UserProfileService(IUnitOfWork unitOfWork,
        IMapper mapper) : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDTO<UserProfileDTO>> GetProfile(string userId)
        {
            try
            {
                var profile = await _unitOfWork.UserProfile.GetAsync(
                    userProfile => userProfile.UserId.Equals(userId)
                    ) ?? throw new Exception("Profile not found!");

                var mappedProfile = _mapper.Map<UserProfileDTO>(profile);

                return new ResponseDTO<UserProfileDTO>(mappedProfile);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<UserProfileDTO>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> UpdateProfile(UserProfileDTO userProfileDTO)
        {
            try
            {
                var profile = await _unitOfWork.UserProfile.GetAsync(
                    filter: userProfile => userProfile.UserId.Equals(userProfileDTO.UserId) &&
                    userProfile.Id.Equals(userProfileDTO.Id)
                    ) ?? throw new Exception("Profile not found!");

                // update fields
                _mapper.Map(userProfileDTO, profile);

                // update
                await _unitOfWork.UserProfile.UpdateAsync(profile);
                await _unitOfWork.SaveAsync();

                var mappedProfile = _mapper.Map<UserProfileDTO>(profile);

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }
    }
}
