using DueDiligenceChecker.Suppliers.Domain.Model.Commands;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierScreeningCommandFromRequestAssembler
{
    public static ExecuteSupplierScreeningCommand ToCommandFromRequest(int supplierId, ExecuteSupplierScreeningRequest request)
    {
        return new ExecuteSupplierScreeningCommand(supplierId, request.Sources);
    }
}

