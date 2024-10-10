using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;
using Security.Enums;

namespace Persistence;

public static class DbInitialData
{
    private const string AdminGuid = "1f1be752-41a8-40b5-b485-2d801e440851";
    private const string UserGuid = "d44f849b-d52e-4d02-9629-c19329bae4dd";

    public static void AddRoles(this ModelBuilder builder)
    {
        // seed base application roles 
        IEnumerable<IdentityRole> roles = Enum.GetValues<Role>()
            .Select(r => new IdentityRole
            {
                Id = ((int)r).ToString(),
                Name = r.ToString(),
                NormalizedName = r.ToString().Normalize(),
                ConcurrencyStamp = r.ToString().Normalize()
            });
        builder.Entity<IdentityRole>().HasData(roles);
    }

    public static void AddUsers(this ModelBuilder builder)
    {
        // create super admin user
        var email = "admin@example.com";
        var name = "admin";
        var password = "Password.123";

        var superAdminUser = new UserEntity
        {
            Id = AdminGuid,
            UserName = email,
            NormalizedUserName = email.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            FirstName = name,
            LastName = name,
            AccessFailedCount = 0
        };
        
        // create basic user
        email = "user@example.com";
        name = "user";

        var basicUser = new UserEntity
        {
            Id = UserGuid,
            UserName = email,
            NormalizedUserName = email.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            FirstName = name,
            LastName = name,
            AccessFailedCount = 0
        };
        
        PasswordHasher<UserEntity> ph = new PasswordHasher<UserEntity>();
        superAdminUser.PasswordHash = ph.HashPassword(superAdminUser, password);
        basicUser.PasswordHash = ph.HashPassword(basicUser, password);

        builder.Entity<UserEntity>().HasData(superAdminUser, basicUser);
    }

    public static void AddUserRoles(this ModelBuilder builder)
    {
        //set super admin user to super admin role
        var adminUserRole = new IdentityUserRole<string>
        {
            RoleId = ((int)Role.SuperAdmin).ToString(),
            UserId = AdminGuid
        };
        
        // set basic user to user role
        var basicUserRole = new IdentityUserRole<string>
        {
            RoleId = ((int)Role.User).ToString(),
            UserId = UserGuid
        };
        
        builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole, basicUserRole);
    }
}