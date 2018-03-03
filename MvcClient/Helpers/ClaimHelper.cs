using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcClient.Helpers
{
  
  public static class ClaimHelper
  {
    private const string ClaimTypeName = "name";
    private const string ClaimTypeRole = "role";

    public static string GetUserName(IEnumerable<Claim> claims)
    {
      return claims.FirstOrDefault(x => x.Type == ClaimTypeName)?.Value;
    }

    public static string GetClaimValue(IEnumerable<Claim> claims, string claimType)
    {
      return claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }

    public static bool IsSystemAdmin(IEnumerable<Claim> claims)
    {
      return claims.FirstOrDefault(x => x.Type == ClaimTypeRole)?.Value == "SystemAdmin";
    }
  }
}
