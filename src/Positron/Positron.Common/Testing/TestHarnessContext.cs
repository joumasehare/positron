using Positron.Common.Models;

namespace Positron.Common.Testing;

public class TestHarnessContext(
    TestHarnessSettings settings,
    List<VehiclePosition> dataList,
    List<SpatialQuery> queryList)
{
    public List<VehiclePosition> DataList { get; } = dataList;
    public List<SpatialQuery> QueryList { get; } = queryList;
    public bool EnableAsync => settings.EnableAsync;
}