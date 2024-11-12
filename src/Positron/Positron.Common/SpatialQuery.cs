namespace Positron.Common;

public class SpatialQuery(Coordinate coordinate, int testId)
{
    public int TestId { get; } = testId;
    public Coordinate Coordinate { get; } = coordinate;

    public static SpatialQuery Parse(string str)
    {
        var parts = str.Split(',');
        return new SpatialQuery(Coordinate.Parse(parts[1], parts[2]), int.Parse(parts[0]));
    }

    public string ToCsv()
    {
        return $"{TestId},{Coordinate.ToCsv()}";
    }
}