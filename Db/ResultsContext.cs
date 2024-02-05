
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace rook_aoc_2016.Db;

public class ResultsContext : DbContext {
    public DbSet<ProblemResult> ProblemResults { get; set; }

    public ResultsContext(DbContextOptions<ResultsContext> ctx) : base(ctx) {}

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<ProblemResult>()
            .ToTable("problem_result")
            .HasKey(e => e.Id);

        mb.Entity<ProblemResult>()
            .Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Year)
            .HasColumnName("year")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Day)
            .HasColumnName("day")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Part)
            .HasColumnName("part")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Input)
            .HasColumnName("input")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Result)
            .HasColumnName("result");

        mb.Entity<ProblemResult>()
            .Property(e => e.Error)
            .HasColumnName("error");

        mb.Entity<ProblemResult>()
            .Property(e => e.ExecutionTime)
            .HasColumnName("execution_time")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Accepted)
            .HasColumnName("accepted")
            .IsRequired();

        mb.Entity<ProblemResult>()
            .Property(e => e.Created)
            .HasColumnName("created")
            .IsRequired();
    }

}