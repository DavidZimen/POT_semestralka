using Persistence.Entity;

namespace Security.Provider;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user);
}