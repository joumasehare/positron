using Positron.Common.Testing;
using Positron.Proof;

var wrapper = new TestHarnessWrapper(args, new ProofSpacialService());
wrapper.Run();