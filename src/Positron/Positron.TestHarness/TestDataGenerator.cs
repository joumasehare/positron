using Positron.Common;

namespace Positron.TestHarness;

public static class TestDataGenerator
{
    public static void GenerateQuery(string path, int amountOfQueries)
    {
        var lookupPositions = new SpatialQuery[amountOfQueries];
        
        for (var i = 0; i < amountOfQueries; i++)
        {
            lookupPositions[i] = new SpatialQuery(new Coordinate()
            {
                Latitude = Random.Shared.NextSingle() * 90 - 90,
                Longitude = Random.Shared.NextSingle() * 180 - 180,
            }, i + 1);

        }

        File.WriteAllLines(path, lookupPositions.Select(i=>i.ToCsv()));
    }
}