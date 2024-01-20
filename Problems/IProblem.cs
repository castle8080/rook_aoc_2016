
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Problems;

public interface IProblem {
    int Day { get; }

    Task<List<ProblemResult>> ExecuteAsync(ProblemInput input);
}