using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCoreJwt.MiddleWare
{
    public class VerifyTokenSenderMiddleWare
    {
        private readonly RequestDelegate next;


        public VerifyTokenSenderMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// This Middleware validates a JWT token, by comparing the senders HostName
        /// To the DB record
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context) 
        {
            // Test if caller is authenticated otherwise they need to get a token
            // windows will handle the authentication automatically and set isAuthenticated to true;
            bool IsAuthenticated = context.User.Identity.IsAuthenticated;
            string AuthenticationType = string.Empty;


            if (IsAuthenticated)
            {     
                 // get the type of authentication
                 AuthenticationType = context.User.Identity.AuthenticationType.ToLower();



                // test if  AuthTYpe is bearer (authenticationTypes.federation)
                if (AuthenticationType == "authenticationtypes.federation")
                {
                    // test if the caller macthes HOSTNAME etc..
                    if (!VerifyBearer(context))
                    {
                        // if not send 401 unauthorised
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                   

                }
                // else check if windows authentication
                else if ((AuthenticationType == "ntlm" || AuthenticationType == "kerberos"))
                {
                    //TODO attach claims for windows users or create method in token service might be better ???
                    // use CreateADLDSClaims in the ClaimsService
                }
            }
             await next(context);


        }


        /// <summary>
        /// This methods ectracts the bearer token from the header and compares to DB, find calller matches db record, todo query on hostname
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //TODO should this be moved to a Verify/Validation Service 
        private bool VerifyBearer(HttpContext context)
        {
            bool IsTokenValid = false;
            //get callers IP
            var RemoteIpAddress = context.Connection.RemoteIpAddress.ToString(); //  returns  ::1 if local
            //TODO change this out to list of allowed hostname IP address
            if (RemoteIpAddress == "::1")  //shows  ::1  for local set this for mule
            {
                String HostName = Dns.GetHostName();
                IHeaderDictionary Headers = context.Request.Headers;

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
