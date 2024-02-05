using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rook_aoc_2016.Models;
using rook_aoc_2016.Services;

using ProblemResultDB = rook_aoc_2016.Db.ProblemResult;

namespace rook_aoc_2016.Controllers;

public class ResultController : Controller
{
    private readonly ILogger<ResultController> _logger;
    private readonly IProblemResultRepository _problemResultRepository;

    public ResultController(ILogger<ResultController> logger, IProblemResultRepository problemResultRepository)
    {
        this._logger = logger;
        this._problemResultRepository = problemResultRepository;
    }

    // $"/result/{Uri.EscapeDataString(r.Id)}/accepted";
    [Route("/result/{id}/accepted")]
    [HttpPost]
    public async Task<IActionResult> UpdateAccepted(string id, [FromForm] int value)
    {
        _logger.LogInformation($"Updating: id={id} request={value}");

        var problemResult = await _problemResultRepository.FindByIdAsync(id);
        if (problemResult == null) {
            // Should return error view.
            throw new Exception("Not found.");
        }
        problemResult.Accepted = value;

        await _problemResultRepository.UpdateAsync(problemResult);

        return PartialView();
    }
}