using Business_Intelligence_ATMs_Focus.Service;
using Microsoft.AspNetCore.Mvc;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class InquiriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Deposits() 
        {
            ViewBag.DrpDwnListSuc = InquiriesService.Instancia.DrpDwn_Sucursales();
            return View();
        }


        public IActionResult Withdrawal()
        {
            ViewBag.DrpDwnListSuc = InquiriesService.Instancia.DrpDwn_Sucursales();
            return View();
        }

        public IActionResult Movements()
        {
            ViewBag.DrpDwnListSuc = InquiriesService.Instancia.DrpDwn_Sucursales();
            return View();
        }


    }

}
