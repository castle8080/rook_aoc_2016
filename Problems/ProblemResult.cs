namespace rook_aoc_2016.Problems;

public class ProblemResult {
    public int Day { get; }

    public int Part { get; }

    public string? Answer { get; }

    public Exception? Error { get; }

    public TimeSpan ExecutionTime { get; }

    public ProblemResult(int day, int part, string answer, TimeSpan executionTime) {
        Day = day;
        Part = part;
        Answer = answer;
        Error = null;
        ExecutionTime = executionTime;
    }

    public ProblemResult(int day, int part, Exception error, TimeSpan executionTime) {
        Day = day;
        Part = part;
        Answer = null;
        Error = error;
        ExecutionTime = executionTime;
    }

}