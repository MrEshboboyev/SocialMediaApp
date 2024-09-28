namespace SocialMediaApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository Comment { get; }
        ILikeRepository Like { get; }
        IPostRepository Post { get; }
        IUserProfileRepository UserProfile { get; }
        Task SaveAsync();
    }
}
