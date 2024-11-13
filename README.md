# Project Description
Positron aims to improve spatial query performance on a large dataset of geo coordinates.

The approach taken involved two steps
- Find an optimized math equation to perform distance checks on a sphere.
- Reduce the number of calculations performed.

# My Solution
The first step was to find an optimized math equation. As I'm not a mathematician I found a performant solution here https://stackoverflow.com/a/51839058/3736063

After testing the new algorithm it was time to reduce the calculations. In the Benchmark each point being queried is tested against the whole dataset. This means 10 queries against 2 000 000 entries results in 20 000 000 checks.

In my approach I decided to do some preprocessing of the dataset and _chuck (group) each point into X Y buckets_. This divides the earth into chucks that can be represented as a dictionary `Dictionary<Point, List<VehiclePosition>> chucks`. This will then be queried first before finer grained queries are run. The breakdown of the solutions is as follows:

1. Chunk the points into X Y buckets (this can be persisted in the dataset to improve performance more).
2. Calculate the chuck a query coordinate is in.
3. Find the closets chucks to the query chuck. The intent is not to get the absolute closest chuck, but all chucks where a possible closets point can live. To do this a magic number `2.2360679774998` (this is the slope distance of a 1;2 length triangle as the lat:long ratio is 1:2) is added to the closest chuck distance. This number is then used to find all qualifying chunks.
4. Loop through the identified chucks and loop through their `List<VehiclePosition>` and do distance checks. The shortest one is the answer.

All results are validated against GeoCoordinate and deviations are logged (none were observed).

## Results
With a tiny amount of queries and an unprocessed dataset the performance is underwhelming; taking slightly longer than the benchmark and comes in at `267ms` total (`249ms` + `18ms`) compared to the proof of `199 ms` (`0ms` + `199ms`). At first this seems bad, but as stated, if some work happens when inserting into the dataset this will be a lot quicker.

Doing the same on a larger amount of queries shows the benefits of my approach.

Please see the included `Report.html` for my run.

### 1 000 queries
Proof takes `13 164ms` (`0ms` + `13 164ms`) compared to `574ms` (`250ms` + `324ms`).

### 1 000 000 queries
Proof takes `11 611 197ms` (`0ms` + `11 611 197ms`) compared to `72 170ms` (`263ms` + `71907ms`). To put this into perspective, the proof took **3 Hours 13 Minutes 31 Seconds** versus the **1 Minutes 12 Seconds** my approach took. 

### Screenshot of the Large Setup

![Screenshot 2024-11-13 at 10-08-16 Report Summary](https://github.com/user-attachments/assets/caa8b79b-026c-4ade-8ec6-212fe58ea50f)

## Drawbacks
There is some pre-processing that is done on the dataset before queries can be run. This can be optimized and stored in the dataset (DB). But given this, this approach is suited for a large volume of queries on a prepared dataset, and is not well suited for a small number queries on a fresh unprocessed dataset.

## Future Improvements
- Chucks can be grouped into larger islands, effectively reducing the amount of calcs even more, I did not have time to do this.
- Code level optimizations using pointers (not going to touch this).
- Tuning the resolution of chunks.
- Probably a lot more.

# Technical Details
## How to run
1. Clone
2. Rebuild (This puts all binaries and dependencies in a shared output folder)
3. Run `Positron.TestHarness` in Visual Studio or one of the `.bat` files in the output directory.

## Project Structure
There are 5 projects

- `Positron.Common` - Shared code
- `Positron.Experimental` - My approach
- `Positron.Naive` - A naive approach with the optimized distance calculation
- `Positron.Proof` - The same GeoCoordinate approach as the benchmark
- `Positron.TestHarness` - The test runner

The `Positron.TestHarness` project defines test setups and `TestScenario`s based on either launch arguments or static code, it will run the appropriate setup and produce an html report and deviation cvs files if there are any. It does this by launching the _test executables_ and reading their _stdout_ to collate results and provide feedback.

Because the _stdout_ of the test executables are read, each test executable's code is wrapped in a `TestHarnessWrapper` to provide a standardized way to get the correct _stdout_ to the harness.

## A Note on Code Quality
I do not consider this production ready, and a lot of small changes and improvements can be made. My mandate was to make a lot of queries faster, and that was my focus. There are a lot of ugly un-optimized code surrounding the actual test executables, again my goal was not to make the TestHarness or anything else fast, just the tests.

**A lot of simplication and cleanup still needs to be done, but I only have so much time.**

## A Note on Threading
As stated just using parallelism wasn't the answer. I did however use is to loop through each query and do each on a different thread. This was done as I did not have until the heat death of the universe to run through a 1 000 000 query scenario. I also applied the same parallelism for all test executables meaning that the results are comparable and differences are purely algorithmic and not brute force threading.

# Closing Thoughts
This took me way longer than expected, as I was barking up the wrong tree for quite some time. Now it feels close to something I would use. This assessment was fun.
