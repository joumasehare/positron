using Positron.Common;

namespace Positron.TestHarness
{
    public class TestCase(
        string testName,
        string pathToExecutable,
        string pathToData,
        string pathToQuery,
        bool allowAsync,
        bool isGroundTruth = false)
    {
        public bool AllowAsync { get; } = allowAsync;
        public string TestName { get; } = testName;
        public string PathToExecutable { get; } = pathToExecutable;
        public string PathToData { get; } = pathToData;
        public string PathToQuery { get; } = pathToQuery;

        public void LogFailedAttempt(int attemptNumber, int totalRunAmount)
        {
            Attempts.Add(new TestCaseAttempt(attemptNumber, totalRunAmount, AttemptResult.Failed));
        }

        public List<TestCaseAttempt> Attempts { get; set; } = [];
        public string ResultFile { get; set; } = null!;

        public void LogSuccessfulAttempt(int attemptNumber, int totalRunAmount, long totalDuration, long setupTime)
        {
            Attempts.Add(new TestCaseAttempt(attemptNumber, totalRunAmount, AttemptResult.Success, totalDuration, setupTime));
        }

        public void LoadResults()
        {
            foreach (var line in File.ReadAllLines(ResultFile))
            {
                TestResults.Add(SpatialQueryResult.Parse(line));
            }
            TestResults.Sort((result, queryResult) => result.TestId.CompareTo(queryResult.TestId) );
        }

        public List<SpatialQueryResult> TestResults { get; set; } = new();
        public bool IsGroundTruth { get; } = isGroundTruth;
        public List<(SpatialQueryResult QueryResult, SpatialQueryResult GroundTruthResult)> Deviations { get; } = new();

        public void ValidateResults(TestCase groundTruth)
        {
            if (groundTruth != null)
                return;

            foreach (var spatialQueryResult in TestResults)
            {
                var corresponding = groundTruth.TestResults.Find(i => i.TestId == spatialQueryResult.TestId);
                if (spatialQueryResult.ClosestVehiclePosition.Id != corresponding.ClosestVehiclePosition.Id)
                {
                    Deviations.Add((spatialQueryResult, corresponding));
                }
            }
        }
    }
}
