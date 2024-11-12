using Positron.Common.Testing;
using Positron.Experimental;

TestHarnessWrapper wrapper = new TestHarnessWrapper(args, new ChunkedGridSpatialService());
wrapper.Run();