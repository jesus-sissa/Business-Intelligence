namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Depositos
    {
        public int Id_Deposito { get; set; }
        public string Nombre_Sucursal { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Hora_Fin { get; set; }
        public string Impote { get; set; }
        public string Usuario_Registro { get; set; }
        public string Status { get; set; }
        public decimal Cantidad_Depositos_MesActual { get; set; }//Se utiliza para el reporte de depositos principal
        public decimal Cantidad_Depositos_MesAnterior { get; set; }//Se utiliza para el reporte de depositos principal
        public int Clave_Sucursal { get; set; }
        public string Deposit_today { get; set; }
    }
}
