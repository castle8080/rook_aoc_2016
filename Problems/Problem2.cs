using System.Text;
using System.Text.RegularExpressions;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Problems;

public class Problem2 : BaseProblem<Problem2> {
    public Problem2(ILogger<Problem2> logger) : base(2, logger) { }

    private static readonly char[][] KEY_PAD_1 = new [] {
        new [] {'1', '2', '3'},
        new [] {'4', '5', '6'},
        new [] {'7', '8', '9'},
    };

    private static readonly char[][] KEY_PAD_2 = new [] {
        new [] {' ', ' ', '1', ' ', ' '},
        new [] {' ', '2', '3', '4', ' '},
        new [] {'5', '6', '7', '8', '9'},
        new [] {' ', 'A', 'B', 'C', ' '},
        new [] {' ', ' ', 'D', ' ', ' '},
    };

    private enum Direction {
        Up,
        Right,
        Down,
        Left,
    }

    private static Direction ParseDirection(char c) {
        switch (c) {
            case 'U': return Direction.Up;
            case 'R': return Direction.Right;
            case 'D': return Direction.Down;
            case 'L': return Direction.Left;
            default:
                throw new Exception($"Invalid direction: {c}.");
        }
    }

    private static List<Direction> Parse(string line) {
        return line.Select(ParseDirection).ToList();
    }

    private static async Task<List<List<Direction>>> ParseInput(ProblemInput input) {
        var lines = await input.ReadLinesAsync();
        return lines
            .Select(l => l.Trim())
            .Where(l => l.Length > 0)
            .Select(Parse)
            .ToList();
    }

    private static Tuple<int, int> Move(char[][] keyPad, int y, int x, Direction d) {
        switch (d) {
            case Direction.Up:
                return Tuple.Create((y > 0 && keyPad[y - 1][x] != ' ') ? y - 1 : y, x);
            case Direction.Down:
                return Tuple.Create((y < keyPad.Length - 1 && keyPad[y + 1][x] != ' ') ? y + 1 : y, x);
            case Direction.Left:
                return Tuple.Create(y, (x > 0 && keyPad[y][x - 1] != ' ') ? x - 1 : x);
            case Direction.Right:
                return Tuple.Create(y, (x < keyPad[y].Length - 1 && keyPad[y][x + 1] != ' ') ? x + 1 : x);
            default:
                throw new Exception($"Invalid dirction: {d}");
        }
    }

    private static string DeterminePassCode(char[][] keyPad, int startY, int startX, List<List<Direction>> directions) {
        var y = startY;
        var x = startX;

        var digits = new List<string>();

        foreach (var directionLine in directions) {
            foreach (var d in directionLine) {
                (y, x) = Move(keyPad, y, x, d);
            }
            digits.Add(keyPad[y][x].ToString());
        }

        return string.Join("", digits);
    }

    public override async Task<string> Part1(ProblemInput input)
    {
        var directions = await ParseInput(input);
        var passcode = DeterminePassCode(KEY_PAD_1, 1, 1, directions);
        return passcode;
    }

    public override async Task<string> Part2(ProblemInput input)
    {
        var directions = await ParseInput(input);
        var passcode = DeterminePassCode(KEY_PAD_2, 2, 0, directions);
        return passcode;
    }
}
