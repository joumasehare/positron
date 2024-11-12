// See https://aka.ms/new-console-template for more information

using Positron.Common.Testing;
using Positron.Naive;

TestHarnessWrapper wrapper = new TestHarnessWrapper(args, new NaiveSpatialService());
wrapper.Run();

/*
Coordinate[] lookupPositions = new Coordinate[10];
lookupPositions[0].Latitude = 34.54491f;
lookupPositions[0].Longitude = -102.100845f;
lookupPositions[1].Latitude = 32.3455429f;
lookupPositions[1].Longitude = -99.12312f;
lookupPositions[2].Latitude = 33.2342339f;
lookupPositions[2].Longitude = -100.214127f;
lookupPositions[3].Latitude = 35.19574f;
lookupPositions[3].Longitude = -95.3489f;
lookupPositions[4].Latitude = 31.89584f;
lookupPositions[4].Longitude = -97.78957f;
lookupPositions[5].Latitude = 32.89584f;
lookupPositions[5].Longitude = -101.789574f;
lookupPositions[6].Latitude = 34.1158371f;
lookupPositions[6].Longitude = -100.225731f;
lookupPositions[7].Latitude = 32.33584f;
lookupPositions[7].Longitude = -99.99223f;
lookupPositions[8].Latitude = 33.53534f;
lookupPositions[8].Longitude = -94.79223f;
lookupPositions[9].Latitude = 32.2342339f;
lookupPositions[9].Longitude = -100.222221f;

/*var lookupPositions = new Coordinate[100000];
for (int i = 0; i < 100000; i++)
{
    lookupPositions[i].Latitude = Random.Shared.NextSingle() * 90 - 90;
    lookupPositions[i].Longitude = Random.Shared.NextSingle() * 180 - 180;
}

List<ISpatialService> spatialServices = [new NaiveSpatialService(), new FlatGridSpatialService()];

Stopwatch stopwatch = Stopwatch.StartNew();
var data = DataFileImporter.ImportVehiclePositions("VehiclePositions.dat");
stopwatch.Stop();
Console.WriteLine($"Read Data Time: {stopwatch.ElapsedMilliseconds}");

foreach (var service in spatialServices)
{
    ConcurrentQueue<(double Distance, VehiclePosition VehiclePosition)> testResults = new();
    
    stopwatch.Restart();
    service.PrepareData(data);
    stopwatch.Stop();
    Console.WriteLine($"Prepare Data Time: {stopwatch.ElapsedMilliseconds}");

    stopwatch.Restart();

    foreach (var coordinate in lookupPositions)
    {
        testResults.Enqueue(service.FindClosestVehiclePosition(coordinate));
    }

    /*Parallel.ForEach(lookupPositions, coordinate =>
    {
        
    });

    stopwatch.Stop();
    Console.WriteLine($"Find Time: {stopwatch.ElapsedMilliseconds}");
    
    foreach (var vehiclePosition in testResults)
    {
        Console.WriteLine($"Result: {vehiclePosition.VehiclePosition}");
        Console.WriteLine($"Distance: {vehiclePosition.Distance}");
    }
}

Console.ReadLine();*/