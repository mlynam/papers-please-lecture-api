using Microsoft.AspNetCore.Authorization;

namespace PapersPlease.Security
{
  public class AgeRequirement : IAuthorizationRequirement
  {
    public AgeRequirement(int minAge)
    {
      MinimumAge = minAge;
    }

    public int MinimumAge { get; }
  }
}
