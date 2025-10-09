using Microsoft.AspNetCore.Mvc;

namespace Almostengr.Common.Controllers;

public abstract class BaseUiController : Controller
{
    protected void AddErrorsToModelState(IReadOnlyList<string> errors)
    {
        foreach (string error in errors)
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

    protected IActionResult NotFoundParitalView()
    {
        return PartialView("_NotFound");
    }

    protected IActionResult NotAuthorizedPartialView()
    {
        return PartialView("_NotAuthorized");
    }
}
