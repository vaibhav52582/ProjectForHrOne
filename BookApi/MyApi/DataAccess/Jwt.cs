﻿using MyApi.DataAccess;
using Microsoft.IdentityModel.Tokens;
using MyApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApi.DataAccess
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Duration { get; set; }

        public Jwt(string? Key, string? Duration)
        {
            this.Key = Key ?? "";
            this.Duration = Duration ?? "";
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("firstname", user.FirstName),
                new Claim("lastname", user.LastName),
                new Claim("mobile", user.Mobile),
                new Claim("email", user.Email),
                new Claim("blocked", user.Blocked.ToString()),
                new Claim("active", user.Active.ToString()),
                new Claim("createdAt", user.CreatedOn),
                new Claim("userType", user.UserType.ToString())
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(this.Duration)),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}