using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetCoreJwt.Services
{

    public class ClaimsService
    {

        public List<Claim> CreateJwtClaims( String UserId, String Role) //use name like mule for anonymous/JWT users
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserId),
                        new Claim(ClaimTypes.Role, Role)
                    };
            return claims;
        }


        //ToDo for ADLDS
        public List<Claim> CreateADLDSClaims(String UserId) //use StaffId for windows.
        {
            // get roles from ADLDA/LDAP & Create Claims
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserId),
                    };
            return claims;
        }


    }
}
