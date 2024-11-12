using Positron.Common;
using Positron.Common.Models;

namespace Positron.Experimental;

internal class DistanceResult
{
    public bool PositionFound { get; set; } = false;
    public double Distance { get; set; }
    public VehiclePosition VehiclePosition { get; set; }
    public Point Chunk { get; set; }
}