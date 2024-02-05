
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