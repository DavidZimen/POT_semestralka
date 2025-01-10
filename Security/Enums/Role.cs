namespace Security.Enums;

public static class Role
{
    public const string Admin = "ROLE_ADMIN";
    public const string Author = "ROLE_AUTHOR";
    public const string User = "ROLE_USER";

    public static bool IsValidRole(string role)
    {
        return role is Admin or Author or User;
    }
}