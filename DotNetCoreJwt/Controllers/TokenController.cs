using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using DotNetCoreJwt.Services.Identity.Claims;

namespace API.Controllers
{
   
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        private IClaimsService _claimsService;


        public TokenController(IConfiguration config, ILogger<TokenController> logger, IClaimsService claimsService)
        {
            _logger = logger;
            _config = config;
            _claimsService=claimsService;
        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateToken(String APIKey)
        {
            // sets default to not authroised http 401
            IActionResult response = Unauthorized();

            var User = string.Empty;
            var Roles = new List<String>();



            // TODO  // check api keys  get from DB
            if (APIKey == "SuperDuperApiKey") 
            {
                User = "Mule"; //TODO get userneme from db
                Roles.Add("Mule");//TODO get role from db/api key


                // get a token
                String TokenString = _claimsService.CreateJwtClaims(User, Roles); 



                //update the http response  to http 200 & send token to caller
                response = Ok(new { token = TokenString });
            }
           
            return response;
        }

        
    }
}