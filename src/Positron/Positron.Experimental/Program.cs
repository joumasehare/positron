using Positron.Common.Testing;
using Positron.Experimental;

var wrapper = new TestHarnessWrapper(args, new ChunkedGridSpatialService());
wrapper.Run();