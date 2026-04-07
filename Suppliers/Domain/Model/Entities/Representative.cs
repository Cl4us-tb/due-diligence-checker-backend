namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities;

public class Representative
{
    public int RepresentativeId { get; private set; }
    public string Role { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int? Age { get; private set; }
    public string? Nationality { get; private set; }

    protected Representative()
    {
        Role = null!;
        FirstName = null!;
        LastName = null!;
    }

    internal Representative(string role, string firstName, string lastName, int? age, string? nationality)
    {
        Role = RequireNonEmpty(role, nameof(role));
        FirstName = RequireNonEmpty(firstName, nameof(firstName));
        LastName = RequireNonEmpty(lastName, nameof(lastName));
        Age = age;
        Nationality = nationality;
    }

    internal void Update(string role, string firstName, string lastName, int? age, string? nationality)
    {
        Role = RequireNonEmpty(role, nameof(role));
        FirstName = RequireNonEmpty(firstName, nameof(firstName));
        LastName = RequireNonEmpty(lastName, nameof(lastName));
        Age = age;
        Nationality = nationality;
    }

    private static string RequireNonEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El valor no puede estar vacío.", paramName);

        return value;
    }
}
