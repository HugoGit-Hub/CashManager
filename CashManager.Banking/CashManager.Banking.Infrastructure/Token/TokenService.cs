﻿using CashManager.Banking.Domain.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashManager.Banking.Infrastructure.Token;

internal class TokenService : ITokenService
{
    private const string Issuer = "CashManager.Banking"; //TODO: set this in config file
    private const string Audience = "CashManager"; //TODO: set this in config file

    public string GenerateToken(Users user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("fnkrjsfnrsjkfsrkfsrpdejfrsfkeopdqebrfsrdfoedifrsbdf"); //TODO: Replace this hard coded token key
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Bank", user.Bank.ToString())
            }),
            Issuer = Issuer,
            Audience = Audience,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}