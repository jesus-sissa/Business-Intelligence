using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class RazonSocialController : Controller
    {
        private readonly IGenericService<razonSocialModel> _SocialReasonService;
        public RazonSocialController(IGenericService<razonSocialModel> SucursalService)
        {
            _SocialReasonService = SucursalService;
        }

        public ActionResult razonSocial()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> listaSocialReason()
        {
            List<razonSocialModel> _lista = await _SocialReasonService.Lista();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarSocialReason([FromBody] razonSocialModel modelo)
        {

            if (ModelState.IsValid)
            {
                bool _resultado = await _SocialReasonService.Guardar(modelo);

                if (_resultado)
                    return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = "Error", msg = "errror" });
            }

        }




        [HttpPut]
        public async Task<IActionResult> editarSocialReason([FromBody] razonSocialModel modelo)
        {

            if (ModelState.IsValid)
            {
                bool _resultado = await _SocialReasonService.Editar(modelo);
                if (_resultado)
                    return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = "Error", msg = "errror" });

            }
        }



    }
}
