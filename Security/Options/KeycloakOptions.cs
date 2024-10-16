namespace Security.Options;

public class KeycloakOptions
{
    public string Realm { get; init; }
    
    public string AuthorizationUrl { get; init; }
    
    public string MetadataAddress { get; init; }
    
    public string ValidIssuer { get; init; }
    
    public string Audience { get; init; }
    
    public string Username { get; init; }
    
    public string Password { get; init; }
    
    public string Url { get; init; }
    
    public string ClientName { get; init; }
}