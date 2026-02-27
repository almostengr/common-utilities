using Almostengr.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Almostengr.Common.Controllers;

public abstract class TestController : Controller
{
    [HttpGet]
    public abstract Task<IActionResult> Index();

    protected async Task<TestResult> ExecuteTest<T>(string name, Func<Task<T>> action)
    {
        TestResult testResult = new()
        {
            TestName = name,
            Passed = false,
        };

        try
        {
            var data = await action();
            testResult.Passed = true;
            testResult.Message = "Passed";
            testResult.Data = data;
        }
        catch (Exception ex)
        {
            testResult.Message = ex.Message;
            testResult.Data = null;
        }

        return testResult;
    }
}
