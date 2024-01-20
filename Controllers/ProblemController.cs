using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using rook_aoc_2016.Models;
using rook_aoc_2016.Problems;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Controllers;

public class ProblemController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProblemService _problems;
    private readonly ProblemInputService _inputService;

    public ProblemController(ILogger<HomeController> logger, ProblemService problems, ProblemInputService inputService)
    {
        _logger = logger;
        _problems = problems;
        _inputService = inputService;
    }

    [Route("/problem/{day}")]
    public async Task<IActionResult> Index(int day)
    {
        try {
            _logger.LogInformation($"Requested day: {day}.");
            var problem = _problems.Get(day);

            var inputs = _inputService.GetInputs(day);
            if (inputs.Count() < 1) {
                throw new Exception($"There are no possible inputs for day: {day}");
            }

            // TODO: Add way to choose input.
            var input = inputs[0];

            var results = await problem.ExecuteAsync(input);

            return PartialView(new ProblemViewModel {
                Day = day,
                Results = results,
                InputName = input.Name,
            });
        }
        catch (Exception e) {
            return PartialView("Error", new ErrorViewModel { Exception = e });
        }
    }
}
