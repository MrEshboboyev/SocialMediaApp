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

            #region Post
            // Post -> PostDTO
            CreateMap<PostDTO, Post>().ReverseMap()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<PostUpdateDTO, Post>();
            #endregion

            #region Comment
            // Comment -> CommentDTO
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap();

            // CommentUpdateDTO -> Comment
            CreateMap<CommentUpdateDTO, Comment>();
            #endregion
        }
    }
}
