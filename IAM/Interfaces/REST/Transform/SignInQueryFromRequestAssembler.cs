using DueDiligenceChecker.IAM.Domain.Model.Queries;
using DueDiligenceChecker.IAM.Interfaces.REST.Resources;
using DueDiligenceChecker.IAM.Domain.Model.Entities;

namespace DueDiligenceChecker.IAM.Interfaces.REST.Transform;

public static class SignInQueryFromRequestAssembler
{
    public static SignInQuery ToQuery(SignInRequest request) => new SignInQuery(request.Email, request.Password);

    public static SignInResponse ToResponse(User user, string token) => new SignInResponse(user.UserId, user.Fullname, user.Email, token);
}

