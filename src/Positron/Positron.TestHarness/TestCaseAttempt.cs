namespace Positron.TestHarness;

public class TestCaseAttempt(
    int attemptNumber,
    int totalRunAmount,
    AttemptResult result,
    long queryTime = 0,
    long setupTime = 0)
{
    public int AttemptNumber { get; } = attemptNumber;
    public int TotalRunAmount { get; } = totalRunAmount;
    public AttemptResult Result { get; } = result;
    public long QueryTime { get; } = queryTime;
    public long SetupTime { get; } = setupTime;
}