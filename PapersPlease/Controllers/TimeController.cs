using Microsoft.AspNetCore.Mvc;
using PapersPlease.Models;
using System;

namespace PapersPlease.Controllers
{
  [ApiController]
  [Route("time")]
  public class TimeController : Controller
  {
    [HttpGet]
    public ActionResult<TimeDTO> Get()
    {
      var time = new TimeDTO
      {
        Iso = DateTime.UtcNow.ToString("o")
      };

      return Ok(time);
    }
  }
}
