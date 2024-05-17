using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public EmployeeController()
        {
            
        }


        [HttpGet("hr")]
        [Authorize(policy: "HumanResourceOrHigher")]
        public IActionResult HumanResourceFunctionTest()
        {
            return Ok("Human Resource Access verified");
        }

    }
}
