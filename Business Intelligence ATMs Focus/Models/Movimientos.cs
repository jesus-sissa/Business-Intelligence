namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Movimientos
    {
        public string Nombre_Sucursal { set; get; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string ImpoteDeposito { get; set; }
        public string ImpoteRetiro { get; set; }
        public string Usuario_Registro { set; get; }
    }
}
