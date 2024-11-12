namespace Positron.Common.Testing;

public class TestHarnessSettings
{
    public const string DefaultDataPath = "VehiclePositions.dat";
    public const string DefaultQueryPath = "100.txt";

    public static TestHarnessSettings Parse(string[] args)
    {
        if (args.Length == 0)
        {
            return new TestHarnessSettings
            {
                DataFilePath = DefaultDataPath,
                LookupFilePath = DefaultQueryPath,
                EnableAsync = false
            };
        }
        return  new TestHarnessSettings
        {
            DataFilePath = args[0],
            LookupFilePath = args[1],
            EnableAsync = bool.Parse(args[2])
        };
    }

    public bool EnableAsync { get; set; }

    public required string LookupFilePath { get; set; }

    public required string DataFilePath { get; set; }

    public string? ResultFileName { get; set; } = "Results.csv";
}