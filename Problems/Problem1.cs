using System.Text;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Problems;

public class Problem1 : BaseProblem {
    public Problem1() : base(1) { }

    public override async Task<string> Part1(ProblemInput input)
    {
        var lines = await input.ReadLinesAsync();
        
        return lines.ToString();
    }

    public override async Task<string> Part2(ProblemInput input)
    {
        return "n/a";
    }
}