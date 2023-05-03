

using Microsoft.AspNetCore.Mvc;

namespace bestmovies_academy.web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View("Admin");
        }
    }
}