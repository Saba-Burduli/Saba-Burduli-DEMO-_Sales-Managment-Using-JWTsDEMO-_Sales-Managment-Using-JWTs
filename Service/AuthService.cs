﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SalesManagementSystem.DAL.Repositories;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.DATA.Settings;
using SalesManagementSystem.SERVICE.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE
{
    public class AuthService 
    {
        //private readonly string _connectionString;
        //private readonly string _jwtSecret; // Store in appsettings.json
        //private readonly IUserRepository _userRepository;
        //private readonly IPasswordHasher _passwordHasher;

        //public AuthService(IConfiguration configuration, IUserRepository userRepository, IPasswordHasher passwordHasher)
        //{
        //    _connectionString = configuration.GetConnectionString("SalesManagementSystemDbConnection");
        //    _jwtSecret = configuration["JwtSettings:JwtSettings"]; // Load from appsettings.json
        //    _userRepository = userRepository;
        //    _passwordHasher = passwordHasher;
        //}

        //public async Task<string> Authenticate(string email, string password)
        //{
        //    var userWithRoles = await _userRepository.GetUserWithRolesByEmailAsync(email);

        //    if (userWithRoles == null || !await _passwordHasher.VerifyPassword(password, userWithRoles.PasswordHash))
        //        throw new ArgumentException("Invalid Credentials");


        //    return GenerateJwtToken(userWithRoles);
        //}

        //private string GenerateJwtToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_jwtSecret);

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName)
        //    };

        //    // Add multiple role claims
        //    foreach (var role in user.Roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        //    }

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddHours(2),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }

    public class TokenService 
    {
    //    private readonly IAppSettings _appSettings;

    //    public TokenService(IAppSettings appSettings)
    //    {
    //        _appSettings = appSettings;
    //    }

    //    public SystemUserModel BuildToken(SystemUserModel user)
    //    {
    //        var claims = GetClaims(user);

    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(claims),

    //            Expires = DateTime.UtcNow.AddDays(7),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };
    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        user.Token = tokenHandler.WriteToken(token);
    //        return user;
    //    }

    //    private Claim[] GetClaims(SystemUserModel user)
    //    {
    //        var claims = new Claim[]
    //       {
    //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //            new Claim(ClaimTypes.Name, user.UserName.ToString()),
    //            //new Claim(ClaimTypes.Role, user.Role.RoleName)
    //       };

    //        //var newClaim = claims.ToList();

    //        //foreach (var role in user.Role.RoleName)
    //        //{
    //        //    newClaim.Add(new Claim(ClaimTypes.Role, role));
    //        //}

    //        return claims;
    //    }

    //    /// <summary>
    //    /// Size of salt.
    //    /// </summary>
    //    private const int SaltSize = 16;

    //    /// <summary>
    //    /// Size of hash.
    //    /// </summary>
    //    private const int HashSize = 20;

    //    /// <summary>
    //    /// Creates a hash from a password.
    //    /// </summary>
    //    /// <param name="password">The password.</param>
    //    /// <param name="iterations">Number of iterations.</param>
    //    /// <returns>The hash.</returns>
    //    private string Hash(string password, int iterations)
    //    {
    //        // Create salt
    //        byte[] salt;
    //        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

    //        // Create hash
    //        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
    //        var hash = pbkdf2.GetBytes(HashSize);

    //        // Combine salt and hash
    //        var hashBytes = new byte[SaltSize + HashSize];
    //        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
    //        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

    //        // Convert to base64
    //        var base64Hash = Convert.ToBase64String(hashBytes);

    //        // Format hash with extra information
    //        return string.Format($"{_appSettings.PasswordHashSecret}{iterations}${base64Hash}");
    //    }

    //    /// <summary>
    //    /// Creates a hash from a password with 10000 iterations
    //    /// </summary>
    //    /// <param name="password">The password.</param>
    //    /// <returns>The hash.</returns>
    //    public string Hash(string password)
    //    {
    //        return Hash(password, 10000);
    //    }

    //    /// <summary>
    //    /// Checks if hash is supported.
    //    /// </summary>
    //    /// <param name="hashString">The hash.</param>
    //    /// <returns>Is supported?</returns>
    //    private bool IsHashSupported(string hashString)
    //    {
    //        return hashString.Contains($"{_appSettings.PasswordHashSecret}");
    //    }

    //    /// <summary>
    //    /// Verifies a password against a hash.
    //    /// </summary>
    //    /// <param name="password">The password.</param>
    //    /// <param name="hashedPassword">The hash.</param>
    //    /// <returns>Could be verified?</returns>
    //    public bool Verify(string password, string hashedPassword)
    //    {
    //        // Check hash
    //        if (!IsHashSupported(hashedPassword))
    //        {
    //            throw new NotSupportedException("The hashtype is not supported");
    //        }

    //        // Extract iteration and Base64 string
    //        var splittedHashString = hashedPassword.Replace($"{_appSettings.PasswordHashSecret}", "").Split('$');
    //        var iterations = int.Parse(splittedHashString[0]);
    //        var base64Hash = splittedHashString[1];

    //        // Get hash bytes
    //        var hashBytes = Convert.FromBase64String(base64Hash);

    //        // Get salt
    //        var salt = new byte[SaltSize];
    //        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

    //        // Create hash with given salt
    //        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
    //        byte[] hash = pbkdf2.GetBytes(HashSize);

    //        // Get result
    //        for (var i = 0; i < HashSize; i++)
    //        {
    //            if (hashBytes[i + SaltSize] != hash[i])
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }
    }
}
