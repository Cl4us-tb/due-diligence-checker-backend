using DueDiligenceChecker.IAM.Domain.Model.Commands;
using DueDiligenceChecker.IAM.Interfaces.REST.Resources;

namespace DueDiligenceChecker.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpRequest request)
    {
        return new SignUpCommand(request.Fullname, request.Email, request.Password);
    }
}


