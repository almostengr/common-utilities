namespace Almostengr.Common.Shared;

public class TestResult
{
    public TestResult()
    {
    }

    public string TestName { get; set; }
    public bool Passed { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
}