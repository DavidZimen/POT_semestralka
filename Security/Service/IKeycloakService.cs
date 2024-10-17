namespace Security.Service;

public interface IKeycloakService
{
    Task CreateRealmIfNotExists();

    Task CreateClientIfNotExists();
}