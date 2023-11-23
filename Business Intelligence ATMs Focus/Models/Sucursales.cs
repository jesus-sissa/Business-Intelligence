namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Sucursales : SucursalesMonitoreo
    {
        public string Clave_Sucursal { get; set; }
        public string Nombre_Sucursal { get; set; }
        public string Status { get; set; }
        public int Minutos_desconexion { set; get; }
        public int Dias_SinDepositar { get; set; }

        public string Alias { get; set; }

        public int NumVal { get; set; }
        public bool Val1 { get; set; }
        public bool Val2 { get; set; }
        public string Logo { get; set; }
    }
    public class SucursalesMonitoreo
    {
        public int Total_Enlinea { set; get; }
        public int Total_Desconectado { set; get; }
    }
}
