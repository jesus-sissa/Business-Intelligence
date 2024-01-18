using Business_Intelligence_ATMs_Focus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using Business_Intelligence_ATMs_Focus.Service;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class OwnBranchesController : Controller
    {
        private readonly IGenericService<OwnBranchesModel> _SucursalService;
        public OwnBranchesController(IGenericService<OwnBranchesModel> SucursalService)
        {
            _SucursalService = SucursalService;
        }
        public ActionResult OwnBranches()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> listaSucursales()
        {
            List<OwnBranchesModel> _lista = await _SucursalService.Lista();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarSucursales([FromBody] OwnBranchesModel modelo)
        {
            bool _resultado = await _SucursalService.Guardar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }
    }
}
