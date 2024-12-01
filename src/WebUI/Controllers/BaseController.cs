using ApplicationCore.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using IResult = ApplicationCore.Utilities.Results.IResult;

namespace WebUI.Controllers;

public class BaseController : Controller
{
    protected IActionResult HandleResponse(IResult result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        if (result.Message == Messages.CategoryNotFound ||
            result.Message == Messages.ProductNotFound ||
            result.Message == Messages.EmptyCategoryList ||
            result.Message == Messages.EmptyProductList ||
            result.Message == Messages.EmptyProductListForCategoryError)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }
}
