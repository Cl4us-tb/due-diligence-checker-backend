using DueDiligenceChecker.IAM.Application.OutboundServices;
using DueDiligenceChecker.IAM.Domain.Model.Commands;
using DueDiligenceChecker.IAM.Domain.Model.Entities;
using DueDiligenceChecker.IAM.Domain.Repositories;
using DueDiligenceChecker.IAM.Application.InboundServices;
using DueDiligenceChecker.Shared.Domain.Repositories;

namespace DueDiligenceChecker.IAM.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(
        IUserRepository userRepository, 
        IHashingService hashingService, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SignUpCommand command)
    {
        if (await _userRepository.ExistsByEmailAsync(command.Email))
        {
            throw new Exception($"El correo {command.Email} ya se encuentra registrado.");
        }

        var hashedPassword = _hashingService.HashPassword(command.Password);

        var user = new User(command.Fullname, command.Email, hashedPassword);

        await _userRepository.AddAsync(user);

        await _unitOfWork.CompleteAsync();
    }
}


