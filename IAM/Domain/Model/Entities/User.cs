namespace DueDiligenceChecker.IAM.Domain.Model.Entities;

public class User
{
    public int UserId { get; private set; }
    public string Fullname { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    protected User() 
{ 
    Fullname = string.Empty;
    Email = string.Empty;
    PasswordHash = string.Empty;
}

    public User(string fullname, string email, string passwordHash)
    {
        Fullname = fullname;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateFullname(string newFullname)
    {
        Fullname = newFullname;
        UpdatedAt = DateTime.UtcNow;
    }
}
