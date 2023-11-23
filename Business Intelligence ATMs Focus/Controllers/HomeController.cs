using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        [Authorize]
        public IActionResult Index()
        {
            ManagementService.Instancia.Escala_Grafica(Data._Sucursales[0].Alias);
            var Sucursales = DepositosService.Instancia.Listar_SucursalesTopVentasDia();
            return View(Sucursales);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ReporteDepositos()
        {
            var oDepositos = DepositosService.Instancia.Listar_DepositosGeneral();
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult PromedioDepositos()
        {
            var oDepositos = DepositosService.Instancia.Monitoreo_sucursales();
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult SucursalesTop()
        {
            var oDepositos = DepositosService.Instancia.Listar_SucursalesTop();
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult DepositosDiaxSucursal(string sucursal)
        {
            var oDepositos = DepositosService.Instancia.ListaDepositosxSucursal(sucursal);
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult UltimosMovimientos()
        {
            var oDepositos = DepositosService.Instancia.ListUltimosDepositosRetiros();
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult UltimosMovimientosDepositos()
        {
            var oDepositos = DepositosService.Instancia.ListUltimosDepositos();
            return Json(oDepositos);
        }
        [Authorize]
        [HttpGet]
        public JsonResult UltimosMovimientosRetiros()
        {
            var oDepositos = DepositosService.Instancia.ListUltimosDepositosRetiros();
            return Json(oDepositos);
        }
        [Authorize]
        public IActionResult Privacy() {

            return View();
        }

    }
}