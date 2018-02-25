using DotNetCoreJwt.Services.APIKey;
using Xunit;

namespace UnitTest
{
  
    public class APIKeysServiceTest
    {
        private readonly APIKeysService _apikeysservice;

        public APIKeysServiceTest()
        {
            _apikeysservice = new APIKeysService();
        }


        [Fact]
        public void ReturnNonEmptyString()
        {
            var result = _apikeysservice.CreateKeys();
            Assert.NotEmpty(result);
        }
    }
}
