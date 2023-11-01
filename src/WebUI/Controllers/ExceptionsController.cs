using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("exceptions")]
public class ExceptionsController : Controller
{
    [HttpGet]
    public IActionResult GetException()
    {
		try
		{
            throw new Exception();
        }
		catch (Exception)
		{
            var errorDetail = "Exception throwed from ExceptionController";
            return StatusCode(500, new {ErrorDetail =  errorDetail});
		}        
    }
}
