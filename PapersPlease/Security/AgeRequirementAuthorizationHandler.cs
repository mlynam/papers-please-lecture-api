using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PapersPlease.Security
{
  public class AgeRequirementAuthorizationHandler : AuthorizationHandler<AgeRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
    {
      if (!context.User.Identity.IsAuthenticated)
      {
        context.Fail();
        return Task.CompletedTask;
      }

      var age = context.User.GetAge();
      if (age > requirement.MinimumAge)
      {
        context.Succeed(requirement);
      }
      else
      {
        context.Fail();
      }

      return Task.CompletedTask;
    }
  }
}
