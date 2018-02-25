using DotNetCoreJwt.Services.Identity.Claims;
using DotNetCoreJwt.Services.Identity.Tokens;

using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace UnitTest
{

    public class ClaimsServiceTest
    {
        public TokensService Token { get; }

        private readonly ClaimsService _claimsservice;


        public ClaimsServiceTest()
        {
             Token = new TokensService();
            _claimsservice = new ClaimsService(Token);
        }


        [Fact]
        public void ReturnClaimsString()
        {
            string UserId = "Xunit";

            var Roles = new List<string>();
            Roles.Add("XunitROle");
            var result = _claimsservice.CreateJwtClaims(UserId, Roles);
            Assert.IsType<ClaimsIdentity>(result);
        }
    }
}
