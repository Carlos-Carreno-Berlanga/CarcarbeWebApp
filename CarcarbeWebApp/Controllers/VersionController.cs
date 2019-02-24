using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        [HttpGet, ]
        public  IActionResult GetVersion()
        {
           
            return Ok("HOLA");
        }
    }
}
