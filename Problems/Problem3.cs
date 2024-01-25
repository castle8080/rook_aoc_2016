using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

using rook_aoc_2016.Services;

namespace rook_aoc_2016.Problems;

public class Problem3 : BaseProblem<Problem3> {
    public Problem3(ILogger<Problem3> logger) : base(3, logger) { }

    private static Regex WHITESPACE_REGEX = new Regex(@"\s+");

    private static bool IsTriangle(Tuple<int, int, int> items) {
        return (items.Item1 + items.Item2) > items.Item3;
    }

    private static List<Tuple<int, int, int>> Reorganize(IList<Tuple<int, int, int>> items) {
        var newItems = new List<Tuple<int, int, int>>();

        for (var i = 0; i < items.Count; i += 3) {
            newItems.Add(Tuple.Create(items[i].Item1, items[i+1].Item1, items[i+2].Item1));
            newItems.Add(Tuple.Create(items[i].Item2, items[i+1].Item2, items[i+2].Item2));
            newItems.Add(Tuple.Create(items[i].Item3, items[i+1].Item3, items[i+2].Item3));
        }

        return newItems;
    }

    private static Tuple<int, int, int> Sort(Tuple<int, int, int> items) {
        var itemsArray = new [] { items.Item1, items.Item2, items.Item3 };
        Array.Sort(itemsArray);
        return Tuple.Create(itemsArray[0], itemsArray[1], itemsArray[2]);
    }

    private static Tuple<int, int, int> ParseLine(string line)
    {
        var parts = WHITESPACE_REGEX.Split(line);
        if (parts.Length != 3) {
            throw new Exception($"Invalid line: {line}");
        }

        return Tuple.Create(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            int.Parse(parts[2])
        );
    }

    private static async Task<IList<Tuple<int, int, int>>> Parse(ProblemInput input)
    {
        return (await input.ReadLinesAsync())
            .Select(l => l.Trim())
            .Where(l => l.Length > 0)
            .Select(ParseLine)
            .ToList();
    }

    public override async Task<string> Part1(ProblemInput input)
    {
        var items = (await Parse(input)).Select(Sort);
        var validTriangles = items.Where(IsTriangle);

        return validTriangles.Count().ToString();
    }

    public override async Task<string> Part2(ProblemInput input)
    {
        var items = Reorganize(await Parse(input)).Select(Sort);
        var validTriangles = items.Where(IsTriangle);

        return validTriangles.Count().ToString();
    }
}
