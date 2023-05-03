using Microsoft.AspNetCore.Mvc;


namespace bestmovies_academy.web.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View("Start");
    }
}
