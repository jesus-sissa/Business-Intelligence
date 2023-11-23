using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class BranchesController : Controller
    {
        public const string SessionKeyName = "_Usuario";
        // GET: BranchesController
        [Authorize(Roles ="Administrador,Soporte")]
        public IActionResult Index(string status)
        {

            //if (status == "On")
            //{
            //    var oLista = SucursalesService.Instancia.Listar().Where(x => x.Minutos_desconexion < 10).ToList();
            //    return View(oLista);
            //}
            //else if (status == "Off")
            //{
            //    var oLista = SucursalesService.Instancia.Listar().Where(x => x.Minutos_desconexion > 10).ToList();
            //    return View(oLista);
            //}
            //else
            //{

            //}
            //var oLista = SucursalesService.Instancia.Listar();
            var oLista = SucursalesService.Instancia.SucursalesXCorporativo1();
            int sucursalesARevisar = 0;
            int sucursalesDesconectadas = 0;
            foreach (var item in oLista)
            {
                if (item.Dias_SinDepositar > 1)
                {
                    sucursalesARevisar++;
                }
                if (item.Minutos_desconexion >10)
                {
                    sucursalesDesconectadas++;
                }
            }
            ViewBag.Revisar = sucursalesARevisar;
            ViewBag.Desconectadas = sucursalesDesconectadas;
            return View(oLista);
        }
        [Authorize(Roles = "Administrador,Soporte")]
        public IActionResult IndexPrb(string status)
        {
            var oLista = SucursalesService.Instancia.SucursalesXCorporativo();
            int sucursalesARevisar = 0;
            int sucursalesDesconectadas = 0;
            foreach (var item in oLista)
            {
                if (item.Dias_SinDepositar > 1)
                {
                    sucursalesARevisar++;
                }
                if (item.Minutos_desconexion > 10)
                {
                    sucursalesDesconectadas++;
                }
            }
            ViewBag.Revisar = sucursalesARevisar;
            ViewBag.Desconectadas = sucursalesDesconectadas;
            return View(oLista);
        }

        [Authorize(Roles = "Administrador,Soporte")]
        public IActionResult Detail(int Clave_Sucursal,string status,string alias) 
        {
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            ViewBag.Status = status;
            ViewBag.Alias = alias;
            ManagementService.Instancia.ChangeConnection(alias);
            var oLista = DepositosService.Instancia.Listar(Clave_Sucursal);
            var oRetiros = RetirosService.Instancia.Listar(Clave_Sucursal);
            var configuracion = ConfiguracionServices.Instancia.Get_Configuracion(Clave_Sucursal.ToString());
           
            ViewBag.Nombre_Sucursal = oLista.Nombre_Sucursal;
            ViewBag.Fecha = oLista.Fecha;
            ViewBag.Hora = oLista.Hora;
            ViewBag.Importe = oLista.Impote;
            ViewBag.Deposit_today = oLista.Deposit_today;

            ViewBag.FechaR = oRetiros.Fecha;
            ViewBag.HoraR = oRetiros.Hora;
            ViewBag.ImporteR = oRetiros.Impote;
            ViewBag.Retreat_today = oRetiros.Retreat_today;
            ViewBag.Lista = oLista;
            return View(configuracion);
            //return View(oLista);
        }


        [Authorize(Roles = "Administrador,Soporte,Usuario")]
        public IActionResult CustomerBranches(string status)
        {
            if (status == "TD")
            {
                ViewBag.Title = "Sucursales Depósitos Hoy";
            }
            else 
            {
                ViewBag.Title = "Sucursales Depósitos Dia Anterior";
            }
            ViewBag.status = status;
            var Sucursales = DepositosService.Instancia.Listar_Sucursales_TopDia(status);
            return View(Sucursales);
        }
        [Authorize(Roles = "Administrador,Soporte,Usuario")]
        public IActionResult CustomerDetail(int Clave_Sucursal, string status)
        {
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            ViewBag.status = status;
            var oLista = DepositosService.Instancia.Listar(Clave_Sucursal);
            var oRetiros = RetirosService.Instancia.Listar(Clave_Sucursal);
            var infoActual = GraficServices.Instancia.InformacionActual(Clave_Sucursal,status);
            //var informacion_pc = ConfiguracionServices.Instancia.Get_InformacionPC(Clave_Sucursal.ToString());
            ViewBag.Nombre_Sucursal = oLista.Nombre_Sucursal;
            ViewBag.Fecha = oLista.Fecha;
            ViewBag.Hora = oLista.Hora;
            ViewBag.Importe = oLista.Impote;
            ViewBag.Deposit_today = oLista.Deposit_today;

            ViewBag.FechaR = oRetiros.Fecha;
            ViewBag.HoraR = oRetiros.Hora;
            ViewBag.ImporteR = oRetiros.Impote;
            ViewBag.Retreat_today = oRetiros.Retreat_today;
            ViewBag.Lista = oLista;
            return View(infoActual);
            //return View(oLista);
        }
        [Authorize(Roles = "Administrador,Usuario")]
        public IActionResult CustomerMonthyReport(int Clave_Sucursal,string status) 
        {
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            ViewBag.status = status;
            var oLista = DepositosService.Instancia.Listar(Clave_Sucursal);
            var oRetiros = RetirosService.Instancia.Listar(Clave_Sucursal);
            var infoActual = GraficServices.Instancia.InformacionMensual(Clave_Sucursal,status);
            //var informacion_pc = ConfiguracionServices.Instancia.Get_InformacionPC(Clave_Sucursal.ToString());
            ViewBag.Nombre_Sucursal = oLista.Nombre_Sucursal;
            ViewBag.Fecha = oLista.Fecha;
            ViewBag.Hora = oLista.Hora;
            ViewBag.Importe = oLista.Impote;
            ViewBag.Deposit_today = oLista.Deposit_today;

            ViewBag.FechaR = oRetiros.Fecha;
            ViewBag.HoraR = oRetiros.Hora;
            ViewBag.ImporteR = oRetiros.Impote;
            ViewBag.Retreat_today = oRetiros.Retreat_today;
            ViewBag.Lista = oLista;
            return View(infoActual);
            //return View();
        }


        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public IActionResult CashDeposits(int Clave_Sucursal,string role,string status,string alias) 
        {
            ViewBag.Role = role;
            ViewBag.status = status;
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            ViewBag.alias = alias;
            return View();
        }
        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public JsonResult ListDeposits(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin) 
        {
            var ListaDepositos = DepositosService.Instancia.Listar_Todos(Clave_Sucursal, Fecha_Inicio, Fecha_Fin);
            return Json(new { data = ListaDepositos});
        }
        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public IActionResult CashWithdrawal(int Clave_Sucursal, string role, string status, string alias) 
        {
            ViewBag.Role = role;
            ViewBag.status = status;
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            ViewBag.alias = alias;
            return View();
        }
        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public JsonResult ListWithdrawals(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin) 
        {
            var ListaDepositos = RetirosService.Instancia.Listar_Todos(Clave_Sucursal, Fecha_Inicio, Fecha_Fin);
            return Json(new { data = ListaDepositos });
        }
        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public IActionResult CashMovements(int Clave_Sucursal) 
        {
            ViewBag.Clave_Sucursal = Clave_Sucursal;
            return View();
        }
        [Authorize(Roles = "Administrador,Usuario,Soporte")]
        public JsonResult ListMovements(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin)
        {
            var ListaDepositos = MovimientosService.Instancia.Listar_Todos(Clave_Sucursal, Fecha_Inicio, Fecha_Fin);
            return Json(new { data = ListaDepositos });
        }

        #region Graficas
        [Authorize]
        [HttpGet]
        public IActionResult WeeklyEffectiveReport(int Clave_Sucursal)
        {
            var report = GraficServices.Instancia.UltimosSieteDias(Clave_Sucursal);
            return Json(report);
        }
        [Authorize]
        [HttpGet]
        public IActionResult WeeklyWithDrawalReport(int Clave_Sucursal)
        {
            var report = GraficServices.Instancia.UltimosSieteDias(Clave_Sucursal);
            return Json(report);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DayDepositReport(int Clave_Sucursal, string status)
        {
            var report = GraficServices.Instancia.Depositos_Hoy(Clave_Sucursal, status);
            return Json(report);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MonthDepositReport(int Clave_Sucursal, string status)
        {
            var report = GraficServices.Instancia.Depositos_Mensual(Clave_Sucursal, status);
            return Json(report);
        }
        [Authorize]
        [HttpGet]
        public IActionResult DayMonthDepositReport(int Clave_Sucursal, string fecha)
        {
            var report = GraficServices.Instancia.Depositos_Dia_Mes(Clave_Sucursal, fecha);
            return Json(report);
        }


        [Authorize]
        [HttpGet]
        public IActionResult DetailDepositReport(int Clave_Sucursal, int Id_Deposito)
        {
            var report = GraficServices.Instancia.Depositos_Desglose(Clave_Sucursal, Id_Deposito);
            return Json(report);
        }

        #endregion

    }
}
