using Positron.Common.Models;

namespace Positron.Common;

public class SpatialQueryResult(int testId, Coordinate coordinate, VehiclePosition? closestVehiclePosition)
{
    public int TestId { get; } = testId;
    public Coordinate Coordinate { get; } = coordinate;
    public VehiclePosition? ClosestVehiclePosition { get; } = closestVehiclePosition;

    public string ToCsv()
    {
        return $"{TestId},{coordinate.Latitude},{coordinate.Longitude},{ClosestVehiclePosition.ToCsv()}";
    }

    public static SpatialQueryResult Parse(string line)
    {
        var parts = line.Split(',');
        SpatialQueryResult result = new SpatialQueryResult(
            int.Parse(parts[0]),
            Coordinate.Parse(parts[1], parts[2]),
            new VehiclePosition()
            {
                Id = int.Parse(parts[3]),
                Registration = parts[4],
                Latitude = float.Parse(parts[5]),
                Longitude = float.Parse(parts[6]),
                RecordedDateTimeUtc = DateTime.Parse(parts[7])
            });
        return result;
    }
}