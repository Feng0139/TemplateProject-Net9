using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TemplateProject.Core.Settings.System;

namespace TemplateProject.Core.Services.Identity;

public interface ITokenClaimService : IScope
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    string GenerateRefreshToken();
}

public class TokenClaimService : ITokenClaimService
{
    private readonly JwtSetting _jwtSetting;

    public TokenClaimService(JwtSetting jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    
        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSetting.ExpireMinutes),
            signingCredentials: credentials
        );
    
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        
        using var rng = RandomNumberGenerator.Create();
        
        rng.GetBytes(randomNumber);
        
        return Convert.ToBase64String(randomNumber);
    }
}