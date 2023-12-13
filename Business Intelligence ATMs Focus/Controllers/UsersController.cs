using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Business_Intelligence_ATMs_Focus.Controllers
{
    public class UsersController : Controller
    {
        public const string SessionKeyName = "_Usuario";
        [Authorize(Roles = "Administrador")]
        public IActionResult Index() 
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Nuevo() 
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Nuevo(Usuario usuario)
        {
            return View();
        }
        // GET: UsersController
        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }

        private bool Validar_Param(string Clave_unica,string Nombre_Sesion, string Password)
        {
            bool validar = false;
            if ( (Clave_unica != null))
            {
                if ( Nombre_Sesion != null)
                {
                    if ( Password != null)
                    {
                        validar = true;
                    }
                }
            }

            return validar;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {

            if (Validar_Param(usuario.Clave_unica,usuario.Nombre_Sesion,usuario.Password))
            {
                string pas = Tools.EncriptacionSHA1(usuario.Password);
                //Usuario oUsuario = UsuariosService.Instancia.Listar().Where(u => u.Clave_unica == usuario.Clave_unica && u.Nombre_Sesion == usuario.Nombre_Sesion && u.Password == pas.ToUpper()).FirstOrDefault();
                Usuario oUsuario = UsuariosService.Instancia.Listar().Where(u => u.Clave_unica == usuario.Clave_unica && u.Nombre_Sesion == usuario.Nombre_Sesion).FirstOrDefault();

                if (oUsuario == null)
                {
                    //usuario no existe
                    ViewBag.Error = "UNLL";
                    ViewBag.Clave_Unica = usuario.Clave_unica;
                    ViewBag.Nombre_Sesion = usuario.Nombre_Sesion;
                    ViewBag.Pwd = usuario.Password;
                    return View();
                }

                if (oUsuario.Password != pas.ToUpper())
                {
                    //usuario pass erroneo
                    ViewBag.Error = "UPERR";
                    return View();
                }

                if (Convert.ToDateTime(oUsuario.Fecha_Expira) < DateTime.Now)
                {
                    //usuario con fecha expiracion vencida
                    ViewBag.Error = "UEX";
                    return View();
                }

                //producion
                Data._databases = ManagementService.Instancia.GetDatabases();
                //pruebas
                //Data._databases = ManagementService.Instancia.GetDatabasesPrb();


                if (oUsuario.Nivel == 3)
                {
                    //cargar sucursales
                    
                    Data.ccliente = oUsuario.Clave_Cliente;
                  
                    string[] Cadena = oUsuario.Cadena.Split(',');
                    Cadena[0] = Tools.Decode(Cadena[0]);
                    Cadena[1] = Tools.Decode(Cadena[1]);
                    Cadena[2] = Tools.Decode(Cadena[2]);
                    Cadena[3] = Tools.Decode(Cadena[3]);
                    Conexion.Sucursal = "Data Source=" + Cadena[0] + "; Initial Catalog=" + Cadena[1] + ";User ID=" + Cadena[2] + ";Password=" + Cadena[3] + ";";
                    Data._Sucursales = SucursalesService.Instancia.SucursalesXCorporativo(Cadena[1].ToUpper());
                   
                }
                else 
                {
                    //cargar sucursales
                    Data._Sucursales = SucursalesService.Instancia.SucursalesXCorporativo();
                }
                
                Data.Nombre_Comercial = oUsuario.Nombre_Comercial;

                //CREAR COOKIE
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, oUsuario.Nombre_Usuario) ,
                    new Claim(ClaimTypes.Role, GetRole(oUsuario.Nivel)) 
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties).Wait();
                //
           
                //revision del rol de usuario
                if (oUsuario.Nivel == 1 || oUsuario.Nivel == 2)
                {
                    return RedirectToAction("Index", "Branches");
                }
                else 
                {
                    return RedirectToAction("Index", "Home");
                }
                    
            }
            else {
                //falta usuario o contraseña
                ViewBag.Error = "FD";
                ViewBag.Clave_Unica = usuario.Clave_unica;
                ViewBag.Nombre_Sesion = usuario.Nombre_Sesion;
                ViewBag.Pwd = usuario.Password;
                return View();
            }


        }

        private string GetRole(int nivel) 
        {
            string role = "";
            if (nivel == 1)
            {
                role = "Administrador";
            } else if (nivel == 2)
            {
                role = "Soporte";
            } else if (nivel ==3)
            {
                role = "Usuario";
            }

            return role;
        }
        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            //return RedirectToAction("Login", "Users");
            return Redirect("/CajerosInteligentes/Users/Login");
        }
    }
}
