using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCoreJwt.MiddleWare
{
    public class WindowsAuthenticationMiddleWare
    {
        private readonly RequestDelegate next;


        public WindowsAuthenticationMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// This Middleware is for for windows autheticated users
        /// We need to create roles from the groups names from ADLDS
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context) 
        {
            // Test if caller is authenticated 
            // windows will handle the authentication automatically and set isAuthenticated to true;
            bool IsAuthenticated = context.User.Identity.IsAuthenticated;
            string AuthenticationType = string.Empty;


            if (IsAuthenticated)
            {     
                 // get the type of authentication
                 AuthenticationType = context.User.Identity.AuthenticationType.ToLower();

              
                // check if windows authentication
                if ((AuthenticationType == "ntlm" || AuthenticationType == "kerberos"))
                {
                    //TODO attach claims for windows users or create method in token service might be better ???
                    // use CreateADLDSClaims in the ClaimsService
                }
            }
             await next(context);


        }



    }
}
