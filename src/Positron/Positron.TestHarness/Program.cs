using System.Diagnostics;
using Positron.TestHarness;
using Positron.TestHarness.Reporting;


var dataFilePath = "VehiclePositions.dat";
var assessmentQueryPath = "Query.txt";
var proofExe = "Positron.Proof.exe";
var naiveExe = "Positron.Naive.exe";
var experimentalExe = "Positron.Experimental.exe";

bool allowAsync = true;

Dictionary<string, Action> setups = new Dictionary<string, Action>()
{
    { "small", SmallSetup },
    { "medium", MediumSetup },
    { "large", LargeSetup }
};

if (args.Length == 2)
{
    allowAsync = bool.Parse(args[1]);
    setups[args[0].ToLower()].Invoke();
}
else
{
    //Run it the way you want
    SmallSetup();
    //MediumSetup();
    //LargeSetup();
}


void SmallSetup()
{
    Stopwatch sw = Stopwatch.StartNew();

    TestScenario assessmentTestManager = new TestScenario("Assessment Dataset",
        $"This scenario queries the 10 provided coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    assessmentTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, assessmentQueryPath, allowAsync, true));
    assessmentTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.RunScenario();

    var smallSampleQuery = "10.txt";
    TestDataGenerator.GenerateQuery(smallSampleQuery, 10);

    TestScenario smallSampleTestManager = new TestScenario("Random 10 Dataset",
        $"This scenario queries 10 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    smallSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, smallSampleQuery, allowAsync, true));
    smallSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.RunScenario();

    sw.Stop();

    ReportGenerator.GenerateReport(sw.Elapsed, assessmentTestManager, smallSampleTestManager);
    ReportGenerator.GenerateDeviationDatasets(assessmentTestManager, smallSampleTestManager);

    Process.Start(@"cmd.exe ", @"/c " + "Report.html");
}

void MediumSetup()
{
    Stopwatch sw = Stopwatch.StartNew();

    TestScenario assessmentTestManager = new TestScenario("Assessment Dataset",
    $"This scenario queries the 10 provided coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
    TestScenarioSettings.Default);

    assessmentTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, assessmentQueryPath, allowAsync, true));
    assessmentTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.RunScenario();

    var smallSampleQuery = "10.txt";
    TestDataGenerator.GenerateQuery(smallSampleQuery, 10);

    TestScenario smallSampleTestManager = new TestScenario("Random 10 Dataset",
        $"This scenario queries 10 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    smallSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, smallSampleQuery, allowAsync, true));
    smallSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.RunScenario();

    var mediumSampleQuery = "100.txt";
    TestDataGenerator.GenerateQuery(mediumSampleQuery, 100);

    TestScenario mediumSampleTestManager = new TestScenario("Random 100 Dataset",
        $"This scenario queries 100 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    mediumSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, mediumSampleQuery, allowAsync, true));
    mediumSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, mediumSampleQuery, allowAsync));
    mediumSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, mediumSampleQuery, allowAsync));
    mediumSampleTestManager.RunScenario();

    var largeSampleQuery = "1000.txt";
    TestDataGenerator.GenerateQuery(largeSampleQuery, 1_000);

    TestScenario largeSampleTestManager = new TestScenario("Random 1 000 Dataset",
        $"This scenario queries 1 000 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    largeSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, largeSampleQuery, allowAsync, true));
    largeSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, largeSampleQuery, allowAsync));
    largeSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, largeSampleQuery, allowAsync));
    largeSampleTestManager.RunScenario();

    sw.Stop();

    ReportGenerator.GenerateReport(sw.Elapsed, assessmentTestManager, smallSampleTestManager, mediumSampleTestManager, largeSampleTestManager);
    ReportGenerator.GenerateDeviationDatasets(assessmentTestManager, smallSampleTestManager, mediumSampleTestManager, largeSampleTestManager);

    Process.Start(@"cmd.exe ", @"/c " + "Report.html");
}

void LargeSetup()
{
    Stopwatch sw = Stopwatch.StartNew();

    TestScenario assessmentTestManager = new TestScenario("Assessment Dataset",
    $"This scenario queries the 10 provided coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
    TestScenarioSettings.Default);

    assessmentTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, assessmentQueryPath, allowAsync, true));
    assessmentTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, assessmentQueryPath, allowAsync));
    assessmentTestManager.RunScenario();

    var smallSampleQuery = "10.txt";
    TestDataGenerator.GenerateQuery(smallSampleQuery, 10);

    TestScenario smallSampleTestManager = new TestScenario("Random 10 Dataset",
        $"This scenario queries 10 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    smallSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, smallSampleQuery, allowAsync, true));
    smallSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, smallSampleQuery, allowAsync));
    smallSampleTestManager.RunScenario();

    var mediumSampleQuery = "1000.txt";
    TestDataGenerator.GenerateQuery(mediumSampleQuery, 1_000);

    TestScenario mediumSampleTestManager = new TestScenario("Random 1 000 Dataset",
        $"This scenario queries 1 000 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    mediumSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, mediumSampleQuery, allowAsync, true));
    mediumSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, mediumSampleQuery, allowAsync));
    mediumSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, mediumSampleQuery, allowAsync));
    mediumSampleTestManager.RunScenario();

    var largeSampleQuery = "10000.txt";
    TestDataGenerator.GenerateQuery(largeSampleQuery, 10_000);

    TestScenario largeSampleTestManager = new TestScenario("Random 10 000 Dataset",
        $"This scenario queries 10 000 randomly generated coordinates against the dataset provided. It runs each test {TestScenarioSettings.Default.TestCaseRunAmount} times. Async Enabled: {allowAsync}.",
        TestScenarioSettings.Default);

    largeSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, largeSampleQuery, allowAsync, true));
    largeSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, largeSampleQuery, allowAsync));
    largeSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, largeSampleQuery, allowAsync));
    largeSampleTestManager.RunScenario();

    var insaneSampleQuery = "1000000.txt";
    TestDataGenerator.GenerateQuery(insaneSampleQuery, 1_000_000);

    TestScenario insaneSampleTestManager = new TestScenario("Random 1 000 000 Dataset",
        $"This scenario queries 1 000 000 randomly generated coordinates against the dataset provided. This test is only run once. Async Enabled: {allowAsync}.",
        new TestScenarioSettings(){ TestCaseRunAmount = 1});

    insaneSampleTestManager.TestCases.Add(new TestCase("GeoCoordinate", proofExe, dataFilePath, insaneSampleQuery, allowAsync, true));
    insaneSampleTestManager.TestCases.Add(new TestCase("Naive", naiveExe, dataFilePath, insaneSampleQuery, allowAsync));
    insaneSampleTestManager.TestCases.Add(new TestCase("ChunkedGridSpatialService", experimentalExe, dataFilePath, insaneSampleQuery, allowAsync));
    insaneSampleTestManager.RunScenario();

    sw.Stop();

    ReportGenerator.GenerateReport(sw.Elapsed, assessmentTestManager, smallSampleTestManager, mediumSampleTestManager, largeSampleTestManager, insaneSampleTestManager);
    ReportGenerator.GenerateDeviationDatasets(assessmentTestManager, smallSampleTestManager, mediumSampleTestManager, largeSampleTestManager, insaneSampleTestManager);

    Process.Start(@"cmd.exe ", @"/c " + "Report.html");
}