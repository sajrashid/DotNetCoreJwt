﻿using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.WebSockets.Internal.Constants;

namespace DotNetCoreJwt.Services.Identity.Tokens
{
    public interface ITokensService
    {
        string CreateToken(List<Claim> Claims);

        bool VerifyBearer(string hostName, dynamic headers);
    }
}
