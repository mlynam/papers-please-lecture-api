using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PapersPlease.Security
{
  public class CustomBearerAuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
  {
    public IDictionary<string, UserProperties> Users { get; } = new Dictionary<string, UserProperties>
    {
      { "Matt Lynam", new UserProperties { Age = 33 } },
      { "Chris Salinas", new UserProperties { Age = 24 } },
      { "David Sanderson", new UserProperties { Age = 30 } }
    };

    public CustomBearerAuthenticationHandler(
      IOptionsMonitor<AuthenticationOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var values = Context.Request.Headers["Name"];
      string key = values == StringValues.Empty ? string.Empty : (string)values;
      if (Users.TryGetValue(key, out UserProperties user))
      {
        var identity = new ClaimsIdentity("Name");
        identity.AddClaim(new Claim("Age", user.Age.ToString()));
        var principal = new ClaimsPrincipal(identity);
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, "Name")));
      }

      return Task.FromResult(AuthenticateResult.Fail(new Exception("User not found")));
    }
  }
}
