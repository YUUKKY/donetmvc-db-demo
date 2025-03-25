using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using donetmvc_db_demo.Models;
using dotnetcoresample.Models;

namespace donetmvc_db_demo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("/user")]
    public IActionResult CreateUser([FromBody] User user)
    {
        Console.WriteLine("Adding user: " + user.Name);
        var userService = new UserService();
        userService.InsertUser(user);
        return Ok();
    }

    [HttpGet]
    [Route("/users")]
    public IActionResult ListUsers()
    {
        var userService = new UserService();
        var users = userService.CurrentUsers();
        return Ok(users);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}