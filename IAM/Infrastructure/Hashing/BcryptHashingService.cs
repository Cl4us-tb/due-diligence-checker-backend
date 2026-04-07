using DueDiligenceChecker.IAM.Application.OutboundServices;
using BCrypt.Net;

namespace DueDiligenceChecker.IAM.Infrastructure.Hashing;

public class BcryptHashingService : IHashingService
{
    public string HashPassword(string password)
    {
        // Genera un hash seguro con un "salt" automÃ¡tico
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
