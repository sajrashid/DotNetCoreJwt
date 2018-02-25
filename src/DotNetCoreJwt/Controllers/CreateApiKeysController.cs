using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DotNetCoreJwt.Services.APIKey;

namespace API.Controllers
{

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



        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return _apikeysservice.CreateKeys() ;
        }


    }
}