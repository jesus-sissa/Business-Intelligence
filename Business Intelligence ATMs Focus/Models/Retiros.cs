namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Retiros
    {
        public int Id_Retiro { get; set; }
        public string Nombre_Sucursal { set; get; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Impote { get; set; }
        public string Usuario_RegistroRetiro { set; get; }
        public string Numero_Remision { set; get; }
        public string Numero_Envase { set; get; }
        public string Status { get; set; }
        public int Retreat_today { get; set; }
    }
}
