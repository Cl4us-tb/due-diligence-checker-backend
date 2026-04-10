namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

public class InterpolScreeningHit
{
    public int InterpolScreeningHitId { get; private set; }
    public string FamilyName { get; private set; }
    public string Forename { get; private set; }
    public string Gender { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public string PlaceOfBirth { get; private set; }
    public string Nationality { get; private set; }
    public string Charges { get; private set; }

    protected InterpolScreeningHit()
    {
        FamilyName = null!;
        Forename = null!;
        Gender = null!;
        PlaceOfBirth = null!;
        Nationality = null!;
        Charges = null!;
    }

    public InterpolScreeningHit(
        string familyName,
        string forename,
        string gender,
        DateOnly? dateOfBirth,
        string placeOfBirth,
        string nationality,
        string charges)
    {
        FamilyName = familyName;
        Forename = forename;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
        Charges = charges;
    }
}
