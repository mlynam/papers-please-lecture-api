using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapersPlease.Security;

namespace PapersPlease.Controllers
{
  [ApiController]
  [Authorize]
  [Route("identity")]
  public class IdentityController : Controller
  {
    [HttpGet]
    public ActionResult<UserProperties> Get()
    {
      var age = User.GetAge();
      var properties = new UserProperties
      { 
        Age = age
      };

      return Ok(properties);
    }
  }
}
