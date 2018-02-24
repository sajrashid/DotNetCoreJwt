using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetCoreJwt.Services
{
    public class TokensService
    {

        public string BuildToken(List<Claim> Claims)
        {
            // JWT Key we can use app.settings
            //TODO get key from Appsetting for dev
            //TODO for live we must use and environment variable
            // Don't forget update the startup if you change it here or we will get a key mismatch
            string JwtSigningKey = "Rather_very_long_key";

            // create a securoty key
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(JwtSigningKey));
            

            // sign the key using specified algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            
            // create the token from the signed credential
            var token = new JwtSecurityToken(
              issuer: "localhost",
              audience: "localhost",
              expires: DateTime.Now.AddMinutes(1),
              signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
