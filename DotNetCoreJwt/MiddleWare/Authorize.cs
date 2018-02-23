using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCoreJwt.MiddleWare
{
    public class Authorize
    {
        private readonly RequestDelegate next;


        public   Authorize(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext context) //DI IOPtions read from appsettings.json
        {
            bool IsAUthentictaed = context.User.Identity.IsAuthenticated;
            string AuthenticationType = context.User.Identity.AuthenticationType.ToLower();
            // test if autheticated && AuthTYpe is bearer
            if ((IsAUthentictaed) && (AuthenticationType == "authenticationTypes.federation"))
            {
                //get IP
                var RemoteIpAddress = context.Connection.RemoteIpAddress.ToString(); //  returns  ::1 if local

                    if (RemoteIpAddress == "::1")  //shows  ::1  for local
                    {
                        var HostName = Dns.GetHostName();
                        var Headers = context.Request.Headers;
                     
                            foreach (var H in Headers)
                            {
                                //look for auth headers with bearer
                                if ((H.Key == "Authorization") && (H.Value.ToString().ToLower().Contains("bearer")))
                                {
                                    // get header value
                                    var Value = H.Value.ToString();
                                    // clean string remove bearer
                                    string Token = Regex.Replace(Value, "bearer ", "", RegexOptions.IgnoreCase);

                                    // TODO
                                    // Now We Have A Clean Token 
                                    // Select Token From Table Where Hostname = Hostname;
                                    // Test If Token Matches
                                    //    If (Token!=Tokenfromdb)
                                    //    { //Someone If Using A Token That Does Not Match The Hostname
                                    //       //Redirect To 401/403
                                    //    }
                                    //}
                                }

                            }

                    }

            }
            // else check if windows // may need to check for CloudAP
            else if((IsAUthentictaed) && (AuthenticationType == "ntlm" || AuthenticationType == "kerberos"  ))
            {
                // attach claims for windows users
            }
            await next(context);
        }

    }
}
