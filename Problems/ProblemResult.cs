namespace rook_aoc_2016.Problems;

public class ProblemResult {
    public string Id { get;  }

    public int Year {get; }

    public int Day { get; }

    public int Part { get; }

    public string? Answer { get; }

    public Exception? Error { get; }

    public TimeSpan ExecutionTime { get; }

    public ProblemResult(string id, int year, int day, int part, string answer, TimeSpan executionTime) {
        Id = id;
        Year = year;
        Day = day;
        Part = part;
        Answer = answer;
        Error = null;
        ExecutionTime = executionTime;
    }

    public ProblemResult(string id, int year, int day, int part, Exception error, TimeSpan executionTime) {
        Id = id;
        Day = day;
        Part = part;
        Answer = null;
        Error = error;
        ExecutionTime = executionTime;
    }

}