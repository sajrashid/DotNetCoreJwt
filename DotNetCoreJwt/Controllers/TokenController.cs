using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using DotNetCoreJwt.Services;

namespace API.Controllers
{
   
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        private ClaimsService _claimsService;
        private TokensService _tokenService;


        public TokenController(IConfiguration config, ILogger<TokenController> logger, ClaimsService claimsService, TokensService tokenService)
        {
            _logger = logger;
            _config = config;
            _claimsService=claimsService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateToken(String APIKey)
        {
            // sets default to not authroised
            IActionResult response = Unauthorized();
            var user = Authenticate(APIKey);

            if (user != null)
            {
                var claims = _claimsService.CreateJwtClaims(user, "Mule"); // get role from db/api key
                var tokenString = _tokenService.BuildToken(claims, "Rather_very_long_key");
                response = Ok(new { token = tokenString });
            }
           
            return response;
        }

       

        private string Authenticate(String APIKey)
        {
            var user = string.Empty;

            if (APIKey == "SuperDuperApiKey") // check api keys froms constants from DB
            {
                user = "Mule";
            }
            return user;
        }

        
    }
}