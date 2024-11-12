using Positron.Common.Testing;
using Positron.Naive;

var wrapper = new TestHarnessWrapper(args, new NaiveSpatialService());
wrapper.Run();