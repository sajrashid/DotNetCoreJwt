using DotNetCoreJwt.Services.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;


namespace DotNetCoreJwt.Services.Identity.Claims
{
    public class ClaimsService:IClaimsService
    {
        private  ITokensService _tokenService;

        public ClaimsService(ITokensService tokensService)
        {
            _tokenService = tokensService;

        }

        public String CreateJwtClaims( String UserId, List<String> Roles) //use name like mule for anonymous/JWT users
        {
            // create a list of claims and add userId 
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserId)
            };
            // add roles to the claims list
            // for every role add a new claim type role
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // Get new token from token service
           string Token = _tokenService.BuildToken(Claims);
           return Token;
        }


        //ToDo for ADLDS
        public List<Claim> CreateADLDSClaims(String UserId) //use StaffId for windows.
        {
            //TOFO get roles from ADLDS/LDAP & Create Claims
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserId),
                    };
            //TODO goto ADLDS service and lookup groups
            // add groups as roles to the claims list
            // for every role add a new claim type role
            //foreach (var role in Roles)
            //{
            //    Claims.Add(new Claim(ClaimTypes.Role, role));
            //}
            return claims;
        }

      
    }
}
