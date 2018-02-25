using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DotNetCoreJwt.Services.Identity.Claims
{
    public interface IClaimsFactory
    {
        List<Claim> CreateJwtClaims(String UserId, List<String> Roles);
        List<Claim> CreateADLDSClaims(String UserId);
    }
}
