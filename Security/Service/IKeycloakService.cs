namespace Security.Service;

public interface IKeycloakService
{
    void CreateRealmIfNotExists();

    void CreateClientIfNotExists();
}