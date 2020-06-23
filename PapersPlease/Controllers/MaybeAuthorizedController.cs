using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PapersPlease.Controllers
{
  [Route("maybe-authorized")]
  [ApiController]
  public class MaybeAuthorizedController : Controller
  {
    [HttpGet("beer")]
    [Authorize("Over21")]
    public IActionResult CanBuyBeer()
    {
      return Ok();
    }

    [HttpGet("rental-car")]
    [Authorize("Over25")]
    public IActionResult CanRentCar()
    {
      return Ok();
    }
  }
}
