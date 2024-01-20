namespace rook_aoc_2016.Services;

using System.Linq;
using rook_aoc_2016.Problems;

public class ProblemService {

    public List<IProblem> Problems { get; private set; }

    public ProblemService(IEnumerable<IProblem> problems) {
        Problems = problems.OrderBy(p => p.Day).ToList();
    }

    public IProblem Get(int day) {
        var problem = Problems.Find(p => p.Day == day);
        if (problem == null) {
            throw new Exception($"Could not find problem for day: {day}");
        }
        return problem;
    }
}