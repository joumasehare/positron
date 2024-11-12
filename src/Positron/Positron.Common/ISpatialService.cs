using Positron.Common.Testing;

namespace Positron.Common;

public interface ISpatialService
{
    void ProvideContext(TestHarnessContext testHarnessContext);
    SpatialQueryResult ExecuteQuery(SpatialQuery query);
}