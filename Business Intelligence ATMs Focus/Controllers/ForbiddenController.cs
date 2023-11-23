using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class ForbiddenController : Controller
    {
        [Authorize]
        public IActionResult Illegal()
        {
            return View();
        }

        public IActionResult Error() 
        {
            return View();
        }

    }
}
