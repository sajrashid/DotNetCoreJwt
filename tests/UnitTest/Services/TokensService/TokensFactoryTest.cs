using DotNetCoreJwt.Services.Identity.Tokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace UnitTest
{

    public class TokenFactoryTest
    {
        TokensFactory TokensFactory = new TokensFactory();



        [Fact]
        public void ValidateToken()
        {
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY="))
            };


            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Xunit"),
                new Claim(ClaimTypes.Role, "Dummy")
            };
            var Token = TokensFactory.CreateToken(Claims);
            // TODO proper test for valid token type
            Assert.IsType<string>(Token);
            Assert.NotEmpty(Token);
            new JwtSecurityTokenHandler().ValidateToken(Token, validationParameters, out validatedToken);
        }
    }
}
