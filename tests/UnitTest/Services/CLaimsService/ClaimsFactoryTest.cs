using DotNetCoreJwt.Services.Identity.Claims;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace UnitTest
{

    public class ClaimsFactoryTest
    {

        

        /// <summary>
        /// Check claims factory is operational and returning valid claims
        /// </summary>
        [Fact]
        public void IsClaimsFactoryCreatingValidClaims()
        {
            ClaimsFactory ClaimsFactory = new ClaimsFactory();
            var Roles = new List<string>
            {
                "XunitRole"
            };
            //Begin tests  
            // create a claim
            var Claims = ClaimsFactory.CreateJwtClaims("Xunit", Roles);
            // test the type is Claim
            Assert.IsType<List<Claim>>(Claims);
        }
    }
}
