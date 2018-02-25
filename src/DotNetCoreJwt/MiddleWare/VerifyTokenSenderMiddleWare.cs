using DotNetCoreJwt.Services.Identity.Tokens;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DotNetCoreJwt.MiddleWare
{
    public class VerifyTokenSenderMiddleWare
    {
        private readonly RequestDelegate next;
        private ITokensFactory _tokenService;

        public VerifyTokenSenderMiddleWare(RequestDelegate next, ITokensFactory tokenservice)
        {
            this.next = next;
            _tokenService = tokenservice;
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
                    //get callers IP
                    var RemoteIpAddress = context.Connection.RemoteIpAddress.ToString(); //  returns  ::1 if local
                    String HostName = Dns.GetHostName();
                    IHeaderDictionary headers = context.Request.Headers;
                    // test if the caller macthes HOSTNAME etc..
                    if (!_tokenService.VerifyBearer(HostName, headers))
                    {
                        // if not send 401 unauthorised
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                   

                }
             
            }
             await next(context);


        }




    }
}
