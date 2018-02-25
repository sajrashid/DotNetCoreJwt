using DotNetCoreJwt.Services.Identity.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace UnitTest
{

    public class TokenServiceTest
    {
        private readonly TokensService _tokenService;

       // private ITokensService _tokenService;

        public TokenServiceTest()
        {
          //  _tokenService = new TokensService();
        }
        //TODO fix this test

        [Fact]
        public void ReturnNonEmptyString()
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Xunit"),
                new Claim(ClaimTypes.Role, "Dummy")
            };
            var result = _tokenService.CreateToken(Claims);
            Assert.NotEmpty(result);
        }
    }
}
