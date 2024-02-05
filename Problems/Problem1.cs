using System.Text;
using System.Text.RegularExpressions;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Problems;

public class Problem1 : BaseProblem<Problem1> {
    public Problem1(ILogger<Problem1> logger) : base(2016, 1, logger) { }

    public enum Direction {
        North = 0,
        East,
        South,
        West,
    }

    public enum Turn {
        Left,
        Right,
    }

    public struct MoveInstruction {
        private static Regex INSTRUCTION_REGEX = new Regex(@"^([RL])(\d+)$");

        public Turn Turn;
        public int Amount;

        public static MoveInstruction Parse(string instructionText) {
            var m = INSTRUCTION_REGEX.Match(instructionText);
            if (!m.Success) {
                throw new Exception($"Invalid instruction: {instructionText}");
            }

            Turn turn;
            switch (m.Groups[1].ToString()) {
                case "R":
                    turn = Turn.Right;
                    break;
                case "L":
                    turn = Turn.Left;
                    break;
                default:
                    throw new Exception($"Impossible.");
            }

            int amount = int.Parse(m.Groups[2].ToString());

            return new MoveInstruction { Turn = turn, Amount = amount };
        }

        public static IList<MoveInstruction> ParseList(string line) {
            return line
                .Split(",")
                .Select(line => line.Trim())
                .Where(line => line.Length > 0)
                .Select(MoveInstruction.Parse)
                .ToList();
        }

        public static async Task<IList<MoveInstruction>> ParseInput(ProblemInput input) {
            var instructions = new List<MoveInstruction>();
            var lines = await input.ReadLinesAsync();
            if (lines.Count() < 1) {
                throw new Exception($"Empty input received.");
            }
            return MoveInstruction.ParseList(lines[0]);
        }
    }

    public class Position {
        public int Y { get; set; }

        public int X { get; set; }

        public Direction Direction { get; set; }

        public Position() {
            X = 0;
            Y = 0;
            Direction = Direction.North;
        }

        public int Distance() {
            return int.Abs(X) + int.Abs(Y);
        }

        public bool MoveUntil(IEnumerable<MoveInstruction> instructions, Func<Position, bool> moveUntil) {
            if (moveUntil(this)) {
                return true;
            }

            foreach (var instr in instructions) {
                Direction = GetNewDirection(Direction, instr.Turn);
                var move = GetMoveFunction();
                for (var i = 0; i < instr.Amount; i++) {
                    move(1);
                    if (moveUntil(this)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Move(IEnumerable<MoveInstruction> moves) {
            foreach (var move in moves) {
                Move(move);
            }
        }

        public void Move(MoveInstruction instr) {
            Direction = GetNewDirection(Direction, instr.Turn);
            GetMoveFunction()(instr.Amount);
        }

        private Action<int> GetMoveFunction() {
            switch (Direction) {
                case Direction.North:
                    return (amount) => Y += amount;
                case Direction.East:
                    return (amount) => X += amount;
                case Direction.South:
                    return (amount) => Y -= amount;
                case Direction.West:
                    return (amount) => X -= amount;
                default:
                    throw new Exception($"Invalid direction: {Direction}.");
            }
        }

        private static Direction GetNewDirection(Direction d, Turn turn) {
            var amount = (turn == Problem1.Turn.Left) ? -1 : 1;
            return (Direction) ((((int) d) + amount + 4) % 4);
        }
    }

    public override async Task<string> Part1(ProblemInput input)
    {
        var instructions = await MoveInstruction.ParseInput(input);
        var position = new Position();
        position.Move(instructions);
        return position.Distance().ToString();
    }

    public override async Task<string> Part2(ProblemInput input)
    {
        var instructions = await MoveInstruction.ParseInput(input);
        var position = new Position();

        var visited = new HashSet<Tuple<int, int>>();

        position.MoveUntil(instructions, (p) => !visited.Add(Tuple.Create(p.X, p.Y)));

        return position.Distance().ToString();
    }
}