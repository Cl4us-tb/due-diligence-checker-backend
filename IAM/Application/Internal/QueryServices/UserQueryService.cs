using DueDiligenceChecker.IAM.Application.OutboundServices;
using DueDiligenceChecker.IAM.Domain.Model.Queries;
using DueDiligenceChecker.IAM.Domain.Repositories;
using DueDiligenceChecker.IAM.Application.InboundServices;

namespace DueDiligenceChecker.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly ITokenService _tokenService;

    public UserQueryService(
        IUserRepository userRepository,
        IHashingService hashingService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _tokenService = tokenService;
    }

    public async Task<(DueDiligenceChecker.IAM.Domain.Model.Entities.User user, string token)?> AuthenticateAsync(SignInQuery query)
    {
        var user = await _userRepository.FindByEmailAsync(query.Email);
        if (user == null) return null;

        if (!_hashingService.VerifyPassword(query.Password, user.PasswordHash)) return null;

        var token = _tokenService.GenerateToken(user);

        return (user, token);
    }
}



