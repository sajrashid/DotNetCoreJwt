using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Microsoft.AspNetCore.WebSockets.Internal.Constants;

namespace DotNetCoreJwt.Services.Identity.Tokens
{
    public class TokensService:ITokensService
    {

        /// <summary>
        /// Build a JWT tokens with a list of claims
        /// </summary>
        /// <param name="Claims"></param>
        /// <returns></returns>
        public string CreateToken(List<Claim> Claims)
        {
            // JWT Key we can use app.settings
            //TODO get key from Appsetting for dev
            //TODO for live we must use and environment variable
            // Don't forget update the startup if you change it here or we will get a key mismatch
            string JwtSigningKey = "fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY=";

            // create a security key
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(JwtSigningKey));


            // sign the key using specified algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // create the token from the signed credentials
            var token = new JwtSecurityToken(
              issuer: "localhost",
              audience: "localhost",
              expires: DateTime.Now.AddMinutes(1),
              signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        /// <summary>
        /// This methods ectracts the bearer token from the header and compares to DB, find calller matches db record, todo query on hostname
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //TODO should this be moved to a Verify/Validation Service 
        public bool VerifyBearer(string HostName, dynamic Headers)
        {
            bool IsTokenValid = false;


            //TODO change this out to list of allowed hostname IP address
            if (HostName == "DESKTOP-34G30MJ")  //shows  ::1  for local set this for mule
            {

                // loop thorugh all header for each header
                foreach (var header in Headers)
                {

                    // test if the auth header = Authorization
                    if (header.Key == "Authorization")
                    {
                        // test the header for word bearer
                        bool IsBearer = header.Value.ToString().ToLower().Contains("bearer");



                        // check TODO's
                        if (IsBearer) // header found!!!
                        {
                            // get header value
                            String Headervalue = header.Value;
                            // we are using regex to remove bearer with ignore case!!!
                            string token = Regex.Replace(Headervalue, "bearer ", "", RegexOptions.IgnoreCase);
                            //now we have a clean token to check against the stored token in the DB




                            // TODO
                            // Select Token From Table Where Hostname = Hostname;
                            // Test If Token Matches
                            //    If (Token!=Tokenfromdb)
                            //    Token does not match 
                            //    { //Someone Is Using A Token That Does Not Match The Hostname!!!!!
                            //       //Redirect To 401/403
                            //    }

                            IsTokenValid = true;
                        }



                    }



                }

            }

            return IsTokenValid;
        }
    }
}
