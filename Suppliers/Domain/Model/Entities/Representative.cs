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
        Age = RequireAdultAgeOrNull(age, nameof(age));
        Nationality = RequireNationalityCodeOrNull(nationality, nameof(nationality));
    }

    internal void Update(string role, string firstName, string lastName, int? age, string? nationality)
    {
        Role = RequireNonEmpty(role, nameof(role));
        FirstName = RequireNonEmpty(firstName, nameof(firstName));
        LastName = RequireNonEmpty(lastName, nameof(lastName));
        Age = RequireAdultAgeOrNull(age, nameof(age));
        Nationality = RequireNationalityCodeOrNull(nationality, nameof(nationality));
    }

    private static string RequireNonEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El valor no puede estar vacío.", paramName);

        return value;
    }

    private static int? RequireAdultAgeOrNull(int? value, string paramName)
    {
        if (value == null) return null;
        if (value < 0) throw new ArgumentException("La edad no puede ser negativa.", paramName);
        if (value < 18) throw new ArgumentException("La edad del representante debe ser mayor o igual a 18.", paramName);
        return value;
    }

    private static string? RequireNationalityCodeOrNull(string? value, string paramName)
    {
        if (value == null) return null;

        var trimmed = value.Trim();
        if (trimmed.Length is < 2 or > 3)
            throw new ArgumentException("La nacionalidad debe ser un código de 2 o 3 letras (ej: PE o PER).", paramName);

        if (!trimmed.All(char.IsLetter))
            throw new ArgumentException("La nacionalidad solo puede contener letras.", paramName);

        return trimmed.ToUpperInvariant();
    }
}

