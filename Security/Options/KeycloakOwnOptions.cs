namespace Security.Options;

public class KeycloakOwnOptions
{
    public required string Realm { get; init; }
    
    public required string AuthorizationUrl { get; init; }
    
    public required string MetadataAddress { get; init; }
    
    public required string ValidIssuer { get; init; }
    
    public required string Audience { get; init; }
    
    public required string Username { get; init; }
    
    public required string Password { get; init; }
    
    public required string Url { get; init; }
    
    public required string ClientName { get; init; }
    
    public required int AccessTokenLifeSpan { get; init; }
}