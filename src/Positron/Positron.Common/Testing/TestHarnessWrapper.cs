using Positron.Common.IO;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Positron.Common.Testing
{
    public class TestHarnessWrapper(string[] args, ISpatialService spatialService)
    {
        public TestHarnessSettings Settings { get; init; } = TestHarnessSettings.Parse(args);

        public void Run()
        {
            try
            {
                StandardOut.LogActionStart(StandardOut.ImportData);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var data = DataFileImporter.ImportVehiclePositions(Settings.DataFilePath);
                stopwatch.Stop();
                StandardOut.LogActionComplete(StandardOut.ImportData, stopwatch.ElapsedMilliseconds);

                StandardOut.LogActionStart(StandardOut.ImportLookupList);
                stopwatch.Restart();
                var queryList = LookupListImporter.LoadLookupList(Settings.LookupFilePath);
                stopwatch.Stop();
                StandardOut.LogActionComplete(StandardOut.ImportLookupList, stopwatch.ElapsedMilliseconds);

                StandardOut.LogSpatialServiceActionStart(StandardOut.SpacialServiceProvideContext, data.Count, queryList.Count);
                stopwatch.Restart();
                spatialService.ProvideContext(new TestHarnessContext(Settings, data, queryList));
                stopwatch.Stop();
                StandardOut.LogActionComplete(StandardOut.SpacialServiceProvideContext, stopwatch.ElapsedMilliseconds);

                StandardOut.LogSpatialServiceActionStart(StandardOut.SpacialServiceExecuteQuery, data.Count, queryList.Count);
                stopwatch.Restart();
                var results = new List<SpatialQueryResult>();
                var progress = 0;
                if (Settings.EnableAsync)
                {
                    ConcurrentBag<SpatialQueryResult> queryResults = [];
                    ParallelOptions options = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount - 1
                    };
                    Parallel.ForEach(queryList, options, query =>
                    {
                        queryResults.Add(spatialService.ExecuteQuery(query));
                        StandardOut.LogProgress(Interlocked.Increment(ref progress), queryList.Count);
                    });

                    results = queryResults.ToList();
                }
                else
                {
                    foreach (var spatialQuery in queryList)
                    {
                        results.Add(spatialService.ExecuteQuery(spatialQuery));
                        progress++;
                        StandardOut.LogProgress(progress, queryList.Count);
                    }
                }
                stopwatch.Stop();
                StandardOut.LogActionComplete(StandardOut.SpacialServiceExecuteQuery, stopwatch.ElapsedMilliseconds);

                StandardOut.LogActionStart(StandardOut.PersistResults);
                stopwatch.Restart();
                var filename = Path.GetFullPath(Settings.ResultFileName);
                File.WriteAllLines(filename, results.Select(r=>r.ToCsv()));
                stopwatch.Stop();
                StandardOut.LogResultsSaved(filename);
                StandardOut.LogActionComplete(StandardOut.PersistResults, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
