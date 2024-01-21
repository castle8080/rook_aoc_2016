using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rook_aoc_2016.Models;
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
    public async Task<IActionResult> Index(int day, [FromQuery(Name = "input")] string? inputName)
    {
        try {
            var problem = _problems.Get(day);

            var inputs = _inputService.GetInputs(day);
            if (inputs.Count() < 1) {
                throw new Exception($"There are no possible inputs for day: {day}");
            }

            ProblemInput input;
            if (inputName == null) {
                input = inputs[0];
            }
            else {
                var _input = inputs.Where(i => i.Name == inputName).First();
                if (_input == null) {
                    throw new Exception($"Unable to find input: {inputName}");
                }
                input = _input;
            }

            var results = await problem.ExecuteAsync(input);

            return PartialView(new ProblemViewModel {
                Day = day,
                Results = results,
                InputName = input.Name,
                PossibleInputs = inputs.Select(input => input.Name).ToList(),
            });
        }
        catch (Exception e) {
            _logger.LogError($"{e}");
            return PartialView("Error", new ErrorViewModel { Exception = e });
        }
    }
}
