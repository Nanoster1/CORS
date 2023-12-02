using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cors.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class TestController : ControllerBase
    {
        public record StrValueDto(string Str);

        [HttpPost("hard")]
        public ActionResult<string> Hard([FromBody] StrValueDto str)
        {
            return str.Str + " from Cors.WebApi";
        }

        [HttpGet("simple")]
        public ActionResult<string> Simple()
        {
            return "Simple request from Cors.WebApi";
        }

        [HttpGet("simple-no-cors"), DisableCors]
        public ActionResult<string> SimpleNoCors()
        {
            return "Simple request from Cors.WebApi";
        }

        [HttpPost("broken"), DisableCors]
        public ActionResult<string> HardBroken()
        {
            return "From Cors.WebApi";
        }
        [HttpOptions("broken")]
        public ActionResult HardBrokenForCors()
        {
            return Ok();
        }

        [HttpPost("custom-header"), EnableCors("NoHeaders")]
        public ActionResult<string> CustomHeader()
        {
            return Request.Headers["X-Custom-Header"].FirstOrDefault() ?? "Not found";
        }

        [HttpGet("exposed-header"), EnableCors("ExposeHeader")]
        public ActionResult ExposedHeader()
        {
            Response.Headers.Add("X-Custom-Header", Request.Headers["X-Custom-Header"].FirstOrDefault() ?? "Not found");
            return Ok();
        }

        [HttpGet("valid-header"), EnableCors("CustomHeader")]
        public ActionResult<string> ValidHeaderGet()
        {
            return Request.Headers["X-Custom-Header"].FirstOrDefault() ?? "Not found";
        }

        [HttpPut("unaccepted-method")]
        public ActionResult<string> UnacceptedMethod([FromBody] StrValueDto str)
        {
            return str.Str + " from Cors.WebApi";
        }
    }
}