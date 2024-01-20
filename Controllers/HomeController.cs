using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using rook_aoc_2016.Models;
using rook_aoc_2016.Problems;
using rook_aoc_2016.Services;

namespace rook_aoc_2016.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProblemService _problems;

    public HomeController(ILogger<HomeController> logger, ProblemService problems)
    {
        _logger = logger;
        _problems = problems;
    }

    [Route("/")]
    public IActionResult Index()
    {
        _logger.LogInformation($"Have problems: {_problems}");
        return View(new HomeViewModel(_problems));
    }
}
