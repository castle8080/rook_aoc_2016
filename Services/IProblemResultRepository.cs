
using rook_aoc_2016.Db;

namespace rook_aoc_2016.Services;

public interface IProblemResultRepository
{

    public Task<IList<ProblemResult>> GetResultsAsync();

    public Task<ProblemResult?> FindByIdAsync(string id);

    public Task<IList<ProblemResult>> GetLatestAcceptedAsync();

    public Task UpdateAsync(ProblemResult problemResult);

    public Task SaveAsync(IList<ProblemResult> results);
}