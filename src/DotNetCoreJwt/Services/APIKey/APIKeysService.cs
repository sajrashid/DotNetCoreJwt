using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DotNetCoreJwt.Services.APIKey
{
    public class APIKeysService : IApiKeysService
    {   
        /// <summary>
        /// Creates a random crptograpic key usefull if you need to create API keys
        /// </summary>
        /// <returns></returns>
        public string CreateKeys()
        {
            string APIKey = string.Empty;
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                APIKey = Convert.ToBase64String(secretKeyByteArray);
            }
            return APIKey;
        }
    }
}
