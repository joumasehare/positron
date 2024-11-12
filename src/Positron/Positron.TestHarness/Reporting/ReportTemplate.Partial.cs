using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.TestHarness.Reporting
{
    public partial class ReportTemplate
    {
        public List<TestScenario> TestScenarios { get; set; }
        public string TotalDuration { get; set; }
    }
}
