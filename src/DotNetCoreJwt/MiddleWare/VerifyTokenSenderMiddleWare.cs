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
            // Test if caller is authenticated 
            bool IsAuthenticated = context.User.Identity.IsAuthenticated;
            string AuthenticationType = string.Empty;


            // if true we need to test if the token has been issued to the current sender/caller
            if (IsAuthenticated)
            {     
                 // get the type of authentication
                 AuthenticationType = context.User.Identity.AuthenticationType.ToLower();



                // test if  AuthTppe is bearer (authenticationTypes.federation)
                if (AuthenticationType == "authenticationtypes.federation")
                {
                    //get callers IP
                    var RemoteIpAddress = context.Connection.RemoteIpAddress.ToString(); //  returns  ::1 if local
                    // do a reverse DNS lookup
                    // TODO time DNS if more that say 20ms we could use the cache DB
                    String HostName = Dns.GetHostName();
                    IHeaderDictionary headers = context.Request.Headers;
                    // test if the caller macthes HOSTNAME etc..
                    if (!_tokenService.VerifyBearer(HostName, headers))
                    {
                        // if token not verfied send 401 unauthorised
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                   

                }
             
            }
             await next(context);


        }




    }
}
