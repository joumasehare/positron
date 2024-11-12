namespace Positron.Common.Testing;

public static class StandardOut
{
    public const string ImportData = "Import Data File";
    public const string ImportLookupList = "Import Lookup File";
    public const string SpacialServiceProvideContext = "Spacial Service Provide Context";
    public const string SpacialServiceExecuteQuery = "Spacial Service Execute Query";
    public const string PersistResults = "Persist Results";

    public static void LogActionStart(string action)
    {
        Console.WriteLine($"ACTION START:{action}");
    }

    public static void LogActionComplete(string action, long duration)
    {
        Console.WriteLine($"ACTION COMPLETE ##{duration}##:{action}");
    }

    public static void LogSpatialServiceActionStart(string spacialServiceProvideContext, int dataCount, int queryListCount)
    {
        Console.WriteLine($"SPATIAL SERVICE ACTION START:{spacialServiceProvideContext}");
        Console.WriteLine($"SPATIAL SERVICE ACTION START DATA COUNT: {dataCount}");
        Console.WriteLine($"SPATIAL SERVICE ACTION START QUERY COUNT: {queryListCount}");
    }

    public static void LogResultsSaved(string filename)
    {
        Console.WriteLine($"RESULT FILE: {filename}");
    }

    public static void LogProgress(int progress, int queryListCount)
    {
        Console.WriteLine($"SPATIAL SERVICE ACTION UPDATE: QUERY ##{progress}## OF ##{queryListCount}##");
    }
}