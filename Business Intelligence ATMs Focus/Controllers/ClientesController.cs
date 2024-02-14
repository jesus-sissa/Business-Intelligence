using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using Business_Intelligence_ATMs_Focus.Service.Implements;
using Microsoft.AspNetCore.Mvc;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IGenericService<CustomerModelo> genericSerice;

        public ClientesController(IGenericService<CustomerModelo> genericSerice)
        {
            this.genericSerice = genericSerice;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clientes()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> guardarCliente([FromBody] CustomerModelo modelo)
        {
            if (ModelState.IsValid)
            {
                bool _resultado = await genericSerice.Guardar(modelo);

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
        public async Task<IActionResult> editarClientes([FromBody] CustomerModelo modelo)
        {


            bool _resultado = await genericSerice.Editar(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });


        }

        public async Task<IActionResult> listaCliente()
        {
            List<CustomerModelo> _lista = await genericSerice.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        public async Task<IActionResult> listaSucursalpropia()
        {

            ServCustomer servCustomer = new ServCustomer();

            List<OwnBranchesModel> _lista = await servCustomer.Lista1();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        public async Task<IActionResult> listaRazonSocial()
        {

            ServCustomer servCustomer = new ServCustomer();

            List<razonSocialModel> _lista = await servCustomer.Lista2();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }
        [HttpPost]
        public async Task<IActionResult> listaClientes([FromBody] OwnBranchesModel modelo)
        {

            ServCustomer servCustomer = new ServCustomer();
            List<CustomerModelo> _lista = null;
            _lista = await servCustomer.ListaBaseDedatos(modelo);
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

    }
}
