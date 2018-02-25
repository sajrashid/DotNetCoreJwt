using System.Collections.Generic;
using System.Security.Claims;

namespace DotNetCoreJwt.Services.Identity.Tokens
{
    public interface ITokensService
    {
        string BuildToken(List<Claim> Claims);
    }
}
