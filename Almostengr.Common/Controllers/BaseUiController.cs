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
public abstract class TestController : Controller
{
    [HttpGet]
    public abstract Task<IActionResult> Index();

    protected async Task<TestResult> ExecuteTest<T>(string name, string domain, Func<Task<T>> action)
    {
        try
        {
            var data = await action();
            return new TestResult{
                TestName = name,
                Passed = true,
                Message = "Passed",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new TestResult{
                TestName = name,
                Passed = false,
                Message = ex.Message,
                Data = null
            };
        }
    }
}