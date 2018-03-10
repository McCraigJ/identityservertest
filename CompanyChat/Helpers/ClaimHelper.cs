using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompanyChat.Helpers
{
  
  public static class ClaimHelper
  {
    private const string ClaimTypeName = "firstname";

    public static string GetFirstName(IEnumerable<Claim> claims)
    {
      return claims.FirstOrDefault(x => x.Type == ClaimTypeName)?.Value;
    }

  }
}
