
using Microsoft.EntityFrameworkCore;
using rook_aoc_2016.Db;

namespace rook_aoc_2016.Services;

public class ProblemResultRepositoryEF : IProblemResultRepository 
{
    private IDbContextFactory<ResultsContext> _dbCtxFactory;

    public ProblemResultRepositoryEF(IDbContextFactory<ResultsContext> dbCtxFactory)
    {
        this._dbCtxFactory = dbCtxFactory;
    }

    public async Task<IList<ProblemResult>> GetLatestAcceptedAsync()
    {
        using (var dbCtx = await _dbCtxFactory.CreateDbContextAsync())
        {

            var maxes = dbCtx.ProblemResults.AsNoTracking()
                .Where(pr => pr.Accepted == 1)
                .GroupBy(pr => new { pr.Year, pr.Day, pr.Part, pr.Input })
                .Select(g => new { Group = g.Key, MaxCreated = g.Max(d => d.Created) });

            var results = await dbCtx.ProblemResults.AsNoTracking()
                .Where(pr => pr.Accepted == 1)
                .Join(
                    maxes,
                    l => new { l.Year, l.Day, l.Part, l.Input, l.Created },
                    r => new { r.Group.Year, r.Group.Day, r.Group.Part, r.Group.Input, Created = r.MaxCreated },
                    (t1, t2) => t1
                )
                .ToListAsync();

            results = results.OrderBy(r => Tuple.Create(r.Year, r.Day, r.Part, r.Input)).ToList();

            return results;
        }
    }

    public async Task<ProblemResult?> FindByIdAsync(string id) {
        using (var dbCtx = await _dbCtxFactory.CreateDbContextAsync())
        {
            return await dbCtx.ProblemResults.FindAsync(id);
        }
    }

    public async Task UpdateAsync(ProblemResult problemResult)
    {
        using (var dbCtx = await _dbCtxFactory.CreateDbContextAsync())
        {
            dbCtx.Add(problemResult);
            dbCtx.Entry(problemResult).State = EntityState.Modified;
            dbCtx.SaveChanges();
        }
    }

    public async Task<IList<ProblemResult>> GetResultsAsync()
    {
        using (var dbCtx = await _dbCtxFactory.CreateDbContextAsync())
        {
            return await dbCtx.ProblemResults.ToListAsync();
        }
    }

    public async Task SaveAsync(IList<ProblemResult> results)
    {
        using (var dbCtx = await _dbCtxFactory.CreateDbContextAsync())
        {
            dbCtx.AddRange(results);
            dbCtx.SaveChanges();
        }
    }
}