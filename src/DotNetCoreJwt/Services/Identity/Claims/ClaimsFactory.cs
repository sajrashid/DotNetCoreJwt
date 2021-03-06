﻿using System;
using System.Collections.Generic;
using System.Security.Claims;


namespace DotNetCoreJwt.Services.Identity.Claims
{
    /// <summary>
    /// This service should handle anything to do with claims
    /// </summary>
    public class ClaimsFactory : IClaimsFactory
    {

       
        /// <summary>
        /// As were creating a JWT claim we send it back complete as a token
        /// the actual token is generated by the token service
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public List<Claim> CreateJwtClaims( String UserId, List<String> Roles) //use name like mule for anonymous/JWT users
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

           return Claims;
        }


        //ToDo for ADLDS
        /// <summary>
        /// lookup groups and create Claims as roles from the groupnames
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Claim> CreateADLDSClaims(String UserId) //use StaffId for windows.
        {
            //TOFO get roles from ADLDS/LDAP & Create Claims
            var Claims = new List<Claim>
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
            return Claims;
        }

      
    }
}
