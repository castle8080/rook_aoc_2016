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

    [Route("/results/latest_accepted")]
    public async Task<IActionResult> LatestAccepted()
    {
        var results = await _problemResultRepository.GetLatestAcceptedAsync();
        return PartialView(new ProblemResultsViewModel { Results = results });
    }

    // $"/result/{Uri.EscapeDataString(r.Id)}/accepted";
    [Route("/result/{id}/accepted")]
    [HttpPost]
    public async Task<IActionResult> UpdateAccepted(string id, [FromForm] int value)
    {
        _logger.LogInformation($"Updating: id={id} request={value}");

        try
        {
            var problemResult = await _problemResultRepository.FindByIdAsync(id);
            if (problemResult == null) {
                _logger.LogError($"Could not find result by id: {id}");

                return PartialView("BasicMessage", new BasicMessageViewModel {
                    Message = "Could not find result by id.",
                    MessageType = BasicMessageType.Error
                });
            }
            problemResult.Accepted = value;

            await _problemResultRepository.UpdateAsync(problemResult);

            // Should return error view.
            return PartialView("BasicMessage", new BasicMessageViewModel {
                Message = "Problem result accepted.",
                MessageType = BasicMessageType.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"Error accepting result: {e}");
            
            // Should return error view.
            return PartialView("BasicMessage", new BasicMessageViewModel {
                Message = $"There was an error updating the problem result: {e.Message}",
                MessageType = BasicMessageType.Error
            });
        }
    }
}