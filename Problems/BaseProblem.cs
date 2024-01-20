
namespace rook_aoc_2016.Problems;

using System.Diagnostics;
using rook_aoc_2016.Services;

public abstract class BaseProblem : IProblem {
    public int Day { get; private set; }

    public BaseProblem(int day) {
        Day = day;
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
        try {
            var result = await f(input);
            return new ProblemResult(Day, part, result, watch.Elapsed);
        }
        catch (Exception e) {
            return new ProblemResult(Day, part, e, watch.Elapsed);
        }
    }
}