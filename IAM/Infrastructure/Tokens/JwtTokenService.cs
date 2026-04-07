using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DueDiligenceChecker.IAM.Application.OutboundServices;
using DueDiligenceChecker.IAM.Domain.Model.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DueDiligenceChecker.IAM.Infrastructure.Tokens;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

public string GenerateToken(User user)
    {
        var secret = _configuration["JwtSettings:Secret"];
        var expirationDays = int.Parse(_configuration["JwtSettings:ExpirationInDays"] ?? "7");

        if (string.IsNullOrEmpty(secret))
            throw new ArgumentNullException(nameof(secret), "El JWT Secret no estÃ¡ configurado.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("email", user.Email),         
                new Claim("fullname", user.Fullname)
            }),
            Expires = DateTime.UtcNow.AddDays(expirationDays),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
