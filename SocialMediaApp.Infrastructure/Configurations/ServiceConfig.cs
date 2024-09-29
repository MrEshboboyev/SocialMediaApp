using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.Infrastructure.Data;
using SocialMediaApp.Infrastructure.Implementations;
using SocialMediaApp.Infrastructure.Repositories;

namespace SocialMediaApp.Infrastructure.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // adding lifetimes
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeService, LikeService>();

            return services;
        }
    }
}
