using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetCoreJwt.Services.Identity.Tokens
{
    public class TokensService:ITokensService
    {


        public string BuildToken(List<Claim> Claims)
        {
            // JWT Key we can use app.settings
            //TODO get key from Appsetting for dev
            //TODO for live we must use and environment variable
            // Don't forget update the startup if you change it here or we will get a key mismatch
            string JwtSigningKey = "fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY=";

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
