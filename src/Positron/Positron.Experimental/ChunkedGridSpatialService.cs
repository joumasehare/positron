using System.Runtime.InteropServices;
using Positron.Common;
using Positron.Common.Helpers;
using Positron.Common.Models;
using Positron.Common.Testing;

namespace Positron.Experimental;

public class ChunkedGridSpatialService : ISpatialService
{
    private TestHarnessContext context;
    private int resolution = 10;
    private int xBound;
    private int yBound;

    private Dictionary<Point, List<VehiclePosition>> chucks = null!;

    public void PrepareData()
    {
        xBound = MathHelpers.LongitudeMax * resolution;
        yBound = MathHelpers.LatitudeMax * resolution;

        chucks = new Dictionary<Point, List<VehiclePosition>>();

        foreach (var vehiclePosition in CollectionsMarshal.AsSpan(context.DataList))
        {
            var resolvedChunk = CalculateChunk(vehiclePosition.Latitude, vehiclePosition.Longitude, resolution);

            if (chucks.ContainsKey(resolvedChunk))
            {
                chucks[resolvedChunk].Add(vehiclePosition);
            }
            else
            {
                chucks.Add(resolvedChunk, [vehiclePosition]);
            }
        }
    }

    public void ProvideContext(TestHarnessContext testHarnessContext)
    {
        context = testHarnessContext;
        PrepareData();
    }

    public SpatialQueryResult ExecuteQuery(SpatialQuery query)
    {
        var queryChunk = CalculateChunk(query.Coordinate, resolution);

        var closetsChunks = GetClosestChunks(queryChunk);
        var shortest = double.MaxValue;
        VehiclePosition? closestVehiclePosition = null;

        foreach (var closet in closetsChunks)
        {
            var results = GetShortestDistanceInChunk(closet, shortest, query.Coordinate);
            if (results.PositionFound && results.Distance < shortest)
            {
                shortest = results.Distance;
                closestVehiclePosition = results.VehiclePosition;
            }
        }

        return new SpatialQueryResult(query.TestId, query.Coordinate, closestVehiclePosition);
    }

    private Point CalculateChunk(float latitude, float longitude, int resolution)
    {
        return new Point()
        {
            X = (int)((longitude + 180) * resolution),
            Y = (int)((latitude + 90) * resolution)
        };
    }

    private Point CalculateChunk(Coordinate coordinate, int resolution)
    {
        return CalculateChunk(coordinate.Latitude, coordinate.Longitude, resolution);
    }

    private IEnumerable<Point> GetClosestChunks(Point point)
    {
        var result = new List<Point>();

        var shortest = double.MaxValue;

        (double distance, Point entry)[] points = new (double distance, Point entry)[chucks.Keys.Count];

        int index = 0;
        foreach (var chuckKey in chucks.Keys)
        {
            points[index] = new ValueTuple<double, Point>(MathHelpers.CalculateDistanceBetweenPoints(chuckKey, point), chuckKey);
            
            if (points[index].distance < shortest)
                shortest = points[index].distance;

            index++;
        }

        var probeDistance = shortest + 2.2360679774998;

        foreach (var valueTuple in points)
        {
            if (valueTuple.distance <= probeDistance)
            {
                result.Add(valueTuple.entry);
            }
        }

        return result;
    }

    private DistanceResult GetShortestDistanceInChunk(Point chunkIndex, double currentShortestDistance, Coordinate coordinate)
    {
        DistanceResult result = new DistanceResult();

        if (!chucks.TryGetValue(chunkIndex, out var chuck))
            return result;

        foreach (var vehiclePosition in chuck)
        {
            var distance = MathHelpers.CalculateDistance(coordinate, vehiclePosition.Coordinate);
            if (distance < currentShortestDistance)
            {
                result.Chunk = new Point()
                {
                    X = chunkIndex.X,
                    Y = chunkIndex.Y,
                };
                result.PositionFound = true;
                result.Distance = distance;
                result.VehiclePosition = vehiclePosition;
                currentShortestDistance = distance;
            }
        }

        return result;
    }
}