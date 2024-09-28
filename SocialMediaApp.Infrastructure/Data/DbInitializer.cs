using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Utility;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data
{
    public class DbInitializer(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AppDbContext db) : IDbInitializer
    {
        // inject Identity Managers
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly AppDbContext _db = db;

        public void Initialize()
        {
            try
            {
                // migrate
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                // If "Admin" role does not exist, create admin user and roles
                if (!_roleManager.RoleExistsAsync(SD.Role_Architect).GetAwaiter().GetResult())
                {
                    // creating roles
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Architect)).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).Wait();

                    // create admin user
                    _userManager.CreateAsync(new AppUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        NormalizedUserName = "ADMIN@EXAMPLE.COM",
                        NormalizedEmail = "ADMIN@EXAMPLE.COM",
                        PhoneNumber = "1112223333",
                    }, "Admin*123").GetAwaiter().GetResult();

                    // finding user and assign role
                    var admin = _db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

                    _userManager.AddToRoleAsync(admin, SD.Role_Architect).GetAwaiter().GetResult();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
