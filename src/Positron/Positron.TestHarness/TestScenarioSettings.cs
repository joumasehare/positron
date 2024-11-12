using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.TestHarness
{
    public class TestScenarioSettings
    {
        public TestScenarioSettings() { }
        public int TestCaseRunAmount { get; set; }
        public static TestScenarioSettings Default => new TestScenarioSettings(){ TestCaseRunAmount = 5 };
    }
}
