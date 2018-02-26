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


        /// <summary>
        /// Test is the token factory is generating valid JWT Tokens
        /// </summary>
        [Fact]
        public void TestIsTokenValid()
        {
            // set up token validation rules
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY="))
            };

            // create some claims
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Xunit"),
                new Claim(ClaimTypes.Role, "Dummy")
            };
            // get a new token from the factory
            var Token = TokensFactory.CreateToken(Claims);
            

            //Begin tests
            // do we have a string ?
            Assert.IsType<string>(Token);
            //make sure it's not empty
            Assert.NotEmpty(Token);
            // validate token is an actual token using the validatin paramters from the setup token validation above
            // it gives a claims prinicpal see https://stackoverflow.com/questions/29355384/when-is-jwtsecuritytokenhandler-validatetoken-actually-valid
            var ClaimsPrinicpal = new JwtSecurityTokenHandler().ValidateToken(Token, validationParameters, out SecurityToken validatedToken);
            // apparently if it returns a principal it means it's valid maybe
            // test if the type is a claims principal
            Assert.IsType<ClaimsPrincipal>(ClaimsPrinicpal);


        }
    }
}
