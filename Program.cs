using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator
{
    public string GenerateToken(string secretKey, string issuer, string audience)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "example_user"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

class Program
{
    static void Main(string[] args)
    {
        string secretKey = "JwtSecretKey@AnimesProtech1234567890";
        string issuer = "localhost";
        string audience = "localhost";

        var tokenGenerator = new JwtTokenGenerator();
        var token = tokenGenerator.GenerateToken(secretKey, issuer, audience);

        Console.WriteLine("Token JWT gerado com sucesso:");
        Console.WriteLine(token);
    }
}
