namespace DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

public record AnnualBilling
{
    public decimal Amount { get; init; }

    public AnnualBilling(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("La facturación anual no puede ser negativa.");

        Amount = amount;
    }
}
