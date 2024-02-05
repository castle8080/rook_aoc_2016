using rook_aoc_2016.Problems;

namespace rook_aoc_2016.Models;

public class ProblemViewModel
{
    public int Day { get; set; }

    public string? InputName { get; set; }

    public IList<string> PossibleInputs { get; set; }

    public List<ProblemResult>? Results { get; set; }

    public string? GetInputURI() {
        if (InputName == null) {
            return null;
        }
        else {
            return $"/input/{Uri.EscapeUriString(InputName)}";
        }
    }

    public string GetDefaultURI() {
        return $"/problem/{Day}";
    }

    public string GetReRunURI() {
        if (InputName == null) {
            return GetDefaultURI();
        }
        else {
            return $"/problem/{Day}?input={Uri.EscapeDataString(InputName)}";
        }
    }

    public string GetAcceptAnswerURI(ProblemResult r) {
        return $"/result/{Uri.EscapeDataString(r.Id)}/accepted";
    }
}
