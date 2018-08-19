using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Laobian.Share.Infrastructure.Identity
{
    public class ClaimsExtractor
    {
        public static string GetHash(IEnumerable<Claim> claims)
        {
            var claim = claims.SingleOrDefault(c => c.Type == IdentityConstant.ClaimTypeHash);
            return claim?.Value;
        }
    }
}