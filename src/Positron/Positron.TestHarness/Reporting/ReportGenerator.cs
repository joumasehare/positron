using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.TestHarness.Reporting
{
    internal static class ReportGenerator
    {
        public static void GenerateReport(TimeSpan totalDuration, params TestScenario[] managers)
        {
            StringBuilder sb = new StringBuilder();

            ReportTemplate template = new ReportTemplate();
            template.TestScenarios = managers.ToList();
            template.TotalDuration = totalDuration.ToString("c");

            File.WriteAllText("Report.html", template.TransformText());
        }

        internal static void GenerateDeviationDatasets(params TestScenario[] managers)
        {
            foreach (var testManager in managers)
            {
                foreach (var testCase in testManager.TestCases)
                {
                    if (testCase.Deviations.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var testCaseDeviation in testCase.Deviations)
                        {
                            sb.Append(testCaseDeviation.QueryResult.ToCsv());
                            sb.Append(',');
                            sb.Append(testCaseDeviation.GroundTruthResult.ToCsv());
                            sb.Append(Environment.NewLine);
                        }
                        File.WriteAllText($"Deviations - {testManager.Name} - {testCase.TestName}.csv", sb.ToString());
                    }
                }
            }
        }
    }

    
}
