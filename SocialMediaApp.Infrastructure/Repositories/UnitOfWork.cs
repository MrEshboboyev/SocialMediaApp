using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Infrastructure.Data;

namespace SocialMediaApp.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private readonly AppDbContext _db = db;

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
