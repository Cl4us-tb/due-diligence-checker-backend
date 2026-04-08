namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record SupplierScreeningResponse(
    int SupplierId,
    DateTime ExecutedAt,
    bool HasHits,
    string SourcesChecked,
    List<InterpolScreeningHitResponse> InterpolHits,
    List<SecopScreeningHitResponse> SecopHits,
    List<SmvScreeningHitResponse> SmvHits);

