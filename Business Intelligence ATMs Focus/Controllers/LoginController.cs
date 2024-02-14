using Microsoft.AspNetCore.Mvc;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }
    }
}
