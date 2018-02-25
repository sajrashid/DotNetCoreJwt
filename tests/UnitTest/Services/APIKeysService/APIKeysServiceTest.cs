using DotNetCoreJwt.Services.APIKey;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Xunit;

namespace UnitTest
{
  
    public class APIKeysServiceTest
    {
       
        APIKeysService APIKeysService = new APIKeysService();
      


        [Fact]
        public void ReturnNonEmptyString()
        {
            string APIKey = string.Empty;
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                APIKey = Convert.ToBase64String(secretKeyByteArray);
            }

            // hard to test can't fake RNGCryptoServiceProvider
            // will test for base64


            var result = APIKeysService.CreateKeys();
            Assert.NotEmpty(result);
            Assert.IsType<string>(result);
            Assert.Matches(@"^[a-zA-Z0-9\+/]*={0,2}$", result); //regex test for valid bas64

        }
    }
}
