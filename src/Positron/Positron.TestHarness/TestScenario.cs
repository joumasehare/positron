using System.Diagnostics;

namespace Positron.TestHarness;

public class TestScenario
{
    public TestScenarioSettings Settings { get; }
    public List<TestCase> TestCases { get; } = new();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TestCase? GroundTruth => TestCases.Find(t => t.IsGroundTruth);

    private TestCase currentTestCase;
    private int currentAttempt;
    private long setupTime;

    public TestScenario(string? name, string? description, TestScenarioSettings settings)
    {
        Settings = settings;
        Name = name;
        Description = description;
    }

    public void RunScenario()
    {
        foreach (var testCase in TestCases)
        {
            currentTestCase = testCase;

            Console.WriteLine($"Initializing Test: {testCase.TestName}");
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = $"\"{testCase.PathToData}\" \"{testCase.PathToQuery}\" {testCase.AllowAsync}";
            processStartInfo.FileName = testCase.PathToExecutable;
            processStartInfo.WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(testCase.PathToExecutable));

            var process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += ProcessOnOutputDataReceived;
            try
            {
                for (int i = 0; i < Settings.TestCaseRunAmount; i++)
                {
                    currentAttempt = i + 1;
                    Console.WriteLine($"Running Test Attempt {i + 1}: {testCase.TestName}");

                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    process.CancelOutputRead();
                }

                Console.WriteLine($"Test Case Completed: {testCase.TestName}");
                currentTestCase.LoadResults();
                Console.WriteLine($"Results Loaded: {testCase.TestName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error running test: {testCase.TestName}");
                Console.WriteLine(e.ToString());
                currentTestCase.LogFailedAttempt(currentAttempt, Settings.TestCaseRunAmount);
            }
            finally
            {
                process.OutputDataReceived -= ProcessOnOutputDataReceived;
            }
        }

        foreach (var testCase in TestCases.Where(t => t.IsGroundTruth == false))
        {
            Console.WriteLine($"Running Test Result Validation: {testCase.TestName}");
            testCase.ValidateResults(GroundTruth);
        }
    }

    private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (String.IsNullOrEmpty(e.Data))
            return;

        if (e.Data.StartsWith("ACTION COMPLETE") && e.Data.Contains("Spacial Service Provide Context"))
        {
            var firstIndex = e.Data.IndexOf("##") + 2;
            var lastIndex = e.Data.LastIndexOf("##");
            setupTime = long.Parse(e.Data.Substring(firstIndex, lastIndex - firstIndex));

        }

        if (e.Data.StartsWith("ACTION COMPLETE") && e.Data.Contains("Spacial Service Execute Query"))
        {
            var firstIndex = e.Data.IndexOf("##") + 2;
            var lastIndex = e.Data.LastIndexOf("##");
            var totalTestCaseTime = long.Parse(e.Data.Substring(firstIndex, lastIndex - firstIndex));

            currentTestCase.LogSuccessfulAttempt(currentAttempt, Settings.TestCaseRunAmount, totalTestCaseTime, setupTime);
        }

        if (e.Data.StartsWith("RESULT FILE:"))
        {
            currentTestCase.ResultFile = e.Data.Substring(e.Data.IndexOf(':') + 1).Trim();
        }

        Console.WriteLine(e.Data);
    }
}