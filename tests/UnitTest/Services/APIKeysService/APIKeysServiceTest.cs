using DotNetCoreJwt.Services.APIKey;
using System;
using System.Security.Cryptography;
using Xunit;

namespace UnitTest
{
  
    public class APIKeysServiceTest
    {
       
        APIKeysService APIKeysService = new APIKeysService();
      

        /// <summary>
        /// // tests API Keys Service
        /// test API Key is an real crypto key matching the creation pattern
        /// of provider/ type of encryption
        /// </summary>
        [Fact]
        public void IsApiKeyServiceCreatingValidCrptoKeys()
        {
            string APIKey = string.Empty;
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                APIKey = Convert.ToBase64String(secretKeyByteArray);
            }

            //create a api key
            //how do we sign this to match the machine/person who owns the key
            var result = APIKeysService.CreateKeys();

            // Begin Test
            // hard to test can't fake RNGCryptoServiceProvider
            // will test for base64 instead 
            // can this be improved ?
            Assert.NotEmpty(result);
            Assert.IsType<string>(result);
            // Regex validates is a base64 string
            Assert.Matches(@"^[a-zA-Z0-9\+/]*={0,2}$", result); //regex test for valid bas64

        }
    }
}
