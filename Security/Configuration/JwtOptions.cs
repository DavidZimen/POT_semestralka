namespace Security.Configuration;

public class JwtOptions
{
    public string Issuer { get; init; }
    
    public string Audience { get; init; }
    
    public string SecretKey { get; init; }
    
    public int ExpirationDays { get; init; }
}