using System.Linq;

namespace DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

public record TaxIdentification
{
    public string Value { get; init; }

    public TaxIdentification(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La identificación tributaria no puede estar vacía.");

        if (value.Length != 11 || !value.All(char.IsDigit))
            throw new ArgumentException("La identificación tributaria debe contener exactamente 11 dígitos numéricos.");

        Value = value;
    }
}
