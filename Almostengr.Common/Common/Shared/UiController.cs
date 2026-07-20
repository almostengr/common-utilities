using Microsoft.AspNetCore.Mvc;

namespace Almostengr.Common.Common.Shared;

public abstract class UiController : Controller
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

    protected IActionResult NotFoundPartialView()
    {
        return PartialView("_NotFound");
    }

    protected IActionResult NotAuthorizedPartialView()
    {
        return PartialView("_NotAuthorized");
    }
}
