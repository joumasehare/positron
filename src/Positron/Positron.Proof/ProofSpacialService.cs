using Positron.Common;
using GeoCoordinatePortable;
using System.Runtime.InteropServices;
using Positron.Common.Models;
using Positron.Common.Testing;

namespace Positron.Proof
{
    internal class ProofSpacialService : ISpatialService
    {
        private TestHarnessContext context;

        public void ProvideContext(TestHarnessContext testHarnessContext)
        {
            context = testHarnessContext;
        }

        public SpatialQueryResult ExecuteQuery(SpatialQuery query)
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(query.Coordinate.Latitude, query.Coordinate.Longitude);
            var shortestPath = double.MaxValue;
            VehiclePosition closestVehiclePosition = null;
            foreach (var position in CollectionsMarshal.AsSpan(context.DataList))
            {
                var distance = geoCoordinate.GetDistanceTo(new GeoCoordinate(position.Latitude, position.Longitude));
                if (distance >= shortestPath) continue;

                shortestPath = distance;
                closestVehiclePosition = position;
            }

            return new SpatialQueryResult(query.TestId, query.Coordinate, closestVehiclePosition);
        }
    }
}
