
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using ProblemResultDTO = rook_aoc_2016.Problems.ProblemResult;

namespace rook_aoc_2016.Db;

public class ProblemResult {

    public string Id { get; set; } = "";

    public int Year { get; set; }

    public int Day { get; set; }

    public int Part { get; set; }

    public string Input { get; set; } = "";

    public string? Result { get; set; }

    public string? Error { get; set; }

    public double ExecutionTime { get; set; }

    public int Accepted { get; set; }

    public DateTime Created { get; set; }

    public static ProblemResult From(ProblemResultDTO pr, string inputName)
    {
        return new ProblemResult {
            Id = pr.Id,
            Year = pr.Year,
            Day = pr.Day,
            Part = pr.Part,
            Input = inputName,
            Result = pr.Answer,
            Error = pr.Error == null ? null : pr.Error.ToString(),
            ExecutionTime = pr.ExecutionTime.TotalSeconds,
            Accepted = 0,
            Created = DateTime.UtcNow
        };
    }
}