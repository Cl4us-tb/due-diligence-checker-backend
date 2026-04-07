using DueDiligenceChecker.IAM.Domain.Model.Commands;

namespace DueDiligenceChecker.IAM.Application.InboundServices;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
}

