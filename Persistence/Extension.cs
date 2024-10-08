using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;
using Security.Enums;

namespace Persistence;

public static class Extension
{
    public static void AddRoles(this ModelBuilder builder)
    {
        // seed base application roles 
        IEnumerable<RoleEntity> roles = Enum.GetValues<Role>()
            .Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString(),
                NormalizedName = r.ToString().Normalize(),
                ConcurrencyStamp = r.ToString().Normalize()
            });
        builder.Entity<RoleEntity>().HasData(roles);
    }

    public static void AddUsers(this ModelBuilder builder)
    {
        // create super admin user
        const string email = "admin@example.com";
        const string name = "admin";
        const string surname = "admin";
        const string password = "Password.123";

        UserEntity superAdminUser = new UserEntity
        {
            Id = 1,
            UserName = email,
            NormalizedUserName = email.Normalize(),
            Email = email,
            NormalizedEmail = email.Normalize(),
            EmailConfirmed = true,
            FirstName = name,
            LastName = surname,
            AccessFailedCount = 0,
        };
        
        PasswordHasher<UserEntity> ph = new PasswordHasher<UserEntity>();
        superAdminUser.PasswordHash = ph.HashPassword(superAdminUser, password);

        builder.Entity<UserEntity>().HasData(superAdminUser);
    }

    public static void AddUserRoles(this ModelBuilder builder, UserManager<UserEntity> roleManager)
    {
        //set super admin user to super admin role
        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { 
            RoleId = (int) Role.SuperAdmin,
            UserId = 1 
        });
    }
}