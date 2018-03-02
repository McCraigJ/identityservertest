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

    public static string GetUserName(IEnumerable<Claim> claims)
    {
      return claims.FirstOrDefault(x => x.Type == ClaimTypeName)?.Value;      
    }
  }
}
