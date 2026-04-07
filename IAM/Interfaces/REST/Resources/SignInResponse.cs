namespace DueDiligenceChecker.IAM.Interfaces.REST.Resources;

public record SignInResponse(int UserId, string Fullname, string Email, string Token);

