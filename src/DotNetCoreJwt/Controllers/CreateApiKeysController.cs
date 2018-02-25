using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DotNetCoreJwt.Services.APIKey;

namespace API.Controllers
{
    //TODO secure controller so only admins can see it

   //TODO - but not yet
    // need to create a form & model for Application name(name of the app that will use API, id sql seed,description summary of caller app, owner name (auto from AD), 
    // Owner EMail auto from AD, Purpose business justification, expected usage droplist, API List (drop down list need to create API table)

    /// <summary>
    /// This Controller returns a valid crptographic key
    /// </summary>    
    [Route("api/[controller]")]
    public class CreateApiKeysController : Controller
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        private IApiKeysService _apikeysservice;


        public CreateApiKeysController(IConfiguration config, ILogger<TokenController> logger, IApiKeysService apikeysservice)
        {
            _logger = logger;
            _config = config;
            _apikeysservice = apikeysservice;
        }


        /// <summary>
        /// aks API Service to create a key and returns a key
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return _apikeysservice.CreateKeys() ;
        }


    }
}