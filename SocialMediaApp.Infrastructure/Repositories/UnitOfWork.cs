using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Infrastructure.Data;

namespace SocialMediaApp.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private readonly AppDbContext _db = db;

        public ICommentRepository Comment { get; private set; } = new CommentRepository(db);
        public ILikeRepository Like { get; private set; } = new LikeRepository(db);
        public IPostRepository Post { get; private set; } = new PostRepository(db);
        public IUserProfileRepository UserProfile { get; private set; } = new UserProfileRepository(db);

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
