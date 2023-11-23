using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class ManagementController : Controller
    {
        // GET: ManagementController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            var infoUser = ManagementService.Instancia.Info();
            var users = ManagementService.Instancia.Get_Users();
            ViewBag.Info = infoUser;
            return View(users);
        }
        [HttpGet]
        public IActionResult ViewUser(int id)
        {
            var user = ManagementService.Instancia.Get_User(id);
            return View(user);
        }

        public IActionResult EditUser(int id)
        {
            ViewBag.DrpDwnListSuc = ManagementService.Instancia.DrpDwn_Clientes();
            ViewBag.DrpDwnListNivel = new List<SelectListItem>
            {
                new SelectListItem {Text = "Administrador", Value = "1"},
                new SelectListItem {Text = "Soporte", Value = "2"},
                new SelectListItem {Text = "Usuario", Value = "3"}
            };
            var user = ManagementService.Instancia.Get_User(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditUser(Usuario usuario)
        {
            var user = ManagementService.Instancia.Update_User(usuario);
            return View(user);
        }
        public IActionResult Clients()
        {
            return View();
        }


    }
}
