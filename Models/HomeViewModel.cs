using rook_aoc_2016.Problems;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Models;

public class HomeViewModel
{
    public ProblemService Problems { get; private set; }

    public HomeViewModel(ProblemService problems) {
        Problems = problems;
    }
}
