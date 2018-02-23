using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetCoreJwt.Services
{
    public class TokensService
    {

        public string BuildToken(List<Claim> Claims, String Key)
        {
           
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: "localhost",
              audience: "localhost",
              expires: DateTime.Now.AddMinutes(1),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
