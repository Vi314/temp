using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetECommerce.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace NetECommerce.Common
{
    public static class Jwt
    {
        public static string GenerateJwtToken(AppUser user)
        {
            //claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b51487ad3be4760cada8cfb4523451c2459f8c398d98ee3657ca4729797195d7"));

            //credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //expires
            var expires = DateTime.Now.AddDays(Convert.ToDouble(365));

            //token
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: "https://localhost:44345",
                audience: "https://localhost:44345",
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );


            //return token
            var result = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);


            return result;
        }
    }
}
