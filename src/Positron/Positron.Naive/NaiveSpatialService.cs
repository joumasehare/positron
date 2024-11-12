using System.Runtime.InteropServices;
using Positron.Common;
using Positron.Common.Helpers;
using Positron.Common.Models;
using Positron.Common.Testing;

namespace Positron.Naive
{
    public class NaiveSpatialService : ISpatialService
    {
        private TestHarnessContext context;

        public void ProvideContext(TestHarnessContext testHarnessContext)
        {
            context = testHarnessContext;
        }

        public SpatialQueryResult ExecuteQuery(SpatialQuery query)
        {
            var shortestPath = double.MaxValue;
            VehiclePosition closestVehiclePosition = null;
            foreach (var position in CollectionsMarshal.AsSpan(context.DataList))
            {
                var distance = MathHelpers.CalculateDistance(query.Coordinate, position.Coordinate);
                if (distance >= shortestPath) continue;

                shortestPath = distance;
                closestVehiclePosition = position;
            }

            return new SpatialQueryResult(query.TestId, query.Coordinate, closestVehiclePosition);
        }
    }
}
