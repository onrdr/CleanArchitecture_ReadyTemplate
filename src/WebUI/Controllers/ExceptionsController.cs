using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("exceptions")]
public class ExceptionsController : Controller
{
    [HttpGet]
    public IActionResult GetException()
    {
        throw new Exception();       
    }
}
