using Microsoft.AspNetCore.Mvc;

namespace Almostengr.Common.Controllers;

public abstract class BaseUiController : Controller
{
    protected void AddErrorsToModelState(IReadOnlyList<string> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }

    protected IActionResult NotFoundView()
    {
        return View("NotFound");
    }

    protected IActionResult NotAuthorizedView()
    {
        return View("NotAuthorized");
    }
}