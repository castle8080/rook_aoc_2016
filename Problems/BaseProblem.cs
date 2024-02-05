
namespace rook_aoc_2016.Problems;

using System.Diagnostics;
using rook_aoc_2016.Services;

public abstract class BaseProblem<T> : IProblem {
    public int Year { get; }

    public int Day { get; }

    public ILogger<T> Logger { get; }

    public BaseProblem(int year, int day, ILogger<T> logger) {
        Year = year;
        Day = day;
        Logger = logger;
    }

    public abstract Task<string> Part1(ProblemInput input);

    public abstract Task<string> Part2(ProblemInput input);

    public async Task<List<ProblemResult>> ExecuteAsync(ProblemInput input) {
        return new List<ProblemResult> {
            await Run(1, input, Part1),
            await Run(2, input, Part2),
        };
    }

    private async Task<ProblemResult> Run(int part, ProblemInput input, Func<ProblemInput, Task<string>> f) {
        var watch = Stopwatch.StartNew();
        var id = Guid.NewGuid().ToString();
        try {
            var result = await f(input);
            return new ProblemResult(id, Year, Day, part, result, watch.Elapsed);
        }
        catch (Exception e) {
            return new ProblemResult(id, Year, Day, part, e, watch.Elapsed);
        }
    }
}