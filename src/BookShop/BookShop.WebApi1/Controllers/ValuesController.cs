using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet(Name = "GetValues")]
        public IActionResult Get()
        {
            var values = new List<string>()
            {
                "1","2","3"
            };
            return Ok(values);
        }
    }
}
