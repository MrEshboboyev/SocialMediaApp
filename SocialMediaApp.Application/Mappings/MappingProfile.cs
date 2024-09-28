using AutoMapper;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UserProfile
            // UserProfile -> UserProfileDTO
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            #endregion
        }
    }
}
