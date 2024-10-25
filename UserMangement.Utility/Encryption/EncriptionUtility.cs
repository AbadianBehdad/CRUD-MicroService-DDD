﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserMangement.Utility.Models;

namespace UserMangement.Utility.Encryption;

public class EncryptionUtility
{
    private readonly Configs _configs;

    public EncryptionUtility(IOptions<Configs> options)
    {
        _configs = options.Value;
    }
    public string GetSHA256(string password, string salt)
    {
        using (var sha256 = SHA256.Create()) 
        {
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
            var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return hash;
        }
    }
    public string GetNewSalt()
    {
        return Guid.NewGuid().ToString();
    }

    public string GetNewToken(Guid UserId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configs.TokenKey);

        var tokenDescriper = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("UserGuid", UserId.ToString()),
                    //new Claim("Time-Minute", tolkenTimeOut.ToString()),
                }),


            Expires = DateTime.UtcNow.AddMinutes(_configs.TokenTimeout),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescriper);
        return tokenHandler.WriteToken(token);
    }

    public string GetNewRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}