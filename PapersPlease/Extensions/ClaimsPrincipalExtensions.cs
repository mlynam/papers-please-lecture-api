using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PapersPlease
{
  public static class ClaimsPrincipalExtensions
  {
    public static int GetAge(this ClaimsPrincipal principal)
    {
      var age = principal.FindFirst("Age");
      if (age == null)
      {
        throw new Exception("age claim not found");
      }

      return int.Parse(age.Value);
    }
  }
}
