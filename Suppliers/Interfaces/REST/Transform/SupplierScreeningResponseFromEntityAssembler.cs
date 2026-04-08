using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierScreeningResponseFromEntityAssembler
{
    public static SupplierScreeningResponse ToResponseFromEntity(SupplierScreening screening)
    {
        return new SupplierScreeningResponse(
            screening.SupplierScreeningId,
            screening.SupplierId,
            screening.ExecutedAt,
            screening.HasHits,
            screening.SourcesChecked,
            screening.InterpolHits.Select(ToResponseFromEntity).ToList(),
            screening.SecopHits.Select(ToResponseFromEntity).ToList(),
            screening.SmvHits.Select(ToResponseFromEntity).ToList());
    }

    public static SupplierScreeningResponse ToResponseFromEntity(SupplierScreening screening, ScreeningSource? sourceFilter)
    {
        if (sourceFilter == null) return ToResponseFromEntity(screening);

        var interpolHits = sourceFilter == ScreeningSource.Interpol
            ? screening.InterpolHits.Select(ToResponseFromEntity).ToList()
            : new List<InterpolScreeningHitResponse>();

        var secopHits = sourceFilter == ScreeningSource.Secop
            ? screening.SecopHits.Select(ToResponseFromEntity).ToList()
            : new List<SecopScreeningHitResponse>();

        var smvHits = sourceFilter == ScreeningSource.Smv
            ? screening.SmvHits.Select(ToResponseFromEntity).ToList()
            : new List<SmvScreeningHitResponse>();

        return new SupplierScreeningResponse(
            screening.SupplierScreeningId,
            screening.SupplierId,
            screening.ExecutedAt,
            screening.HasHits,
            screening.SourcesChecked,
            interpolHits,
            secopHits,
            smvHits);
    }

    private static InterpolScreeningHitResponse ToResponseFromEntity(InterpolScreeningHit hit)
    {
        return new InterpolScreeningHitResponse(
            hit.FamilyName,
            hit.Forename,
            hit.Gender,
            hit.DateOfBirth,
            hit.PlaceOfBirth,
            hit.Nationality,
            hit.Charges);
    }

    private static SecopScreeningHitResponse ToResponseFromEntity(SecopScreeningHit hit)
    {
        return new SecopScreeningHitResponse(
            hit.EntityName,
            hit.EntityTaxId,
            hit.Level,
            hit.Order,
            hit.Municipality,
            hit.ResolutionNumber,
            hit.ContractorDocument,
            hit.ContractorName,
            hit.ContractNumber,
            hit.SanctionAmount,
            hit.PublishedAt,
            hit.FinalizedAt,
            hit.LoadedAt,
            hit.ProcessUrl);
    }

    private static SmvScreeningHitResponse ToResponseFromEntity(SmvScreeningHit hit)
    {
        return new SmvScreeningHitResponse(
            hit.Date,
            hit.Resolution,
            hit.Summary,
            hit.Type,
            hit.Amount,
            hit.WithAppeal,
            hit.ResolutiveResolutionNumber,
            hit.ResolutiveResolutionDate);
    }
}
