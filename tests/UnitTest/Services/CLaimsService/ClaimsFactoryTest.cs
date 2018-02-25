using DotNetCoreJwt.Services.Identity.Claims;
using DotNetCoreJwt.Services.Identity.Tokens;

using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace UnitTest
{

    public class ClaimsFactoryTest
    {

        

        ClaimsFactory ClaimsFactory = new ClaimsFactory();

        [Fact]
        public void ReturnClaimsString()
        {

            var Roles = new List<string>
            {
                "XunitRole"
            };
            var result = ClaimsFactory.CreateJwtClaims("Xunit", Roles);
            Assert.IsType<List<Claim>>(result);
        }
    }
}
