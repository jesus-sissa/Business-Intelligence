namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre_Sesion { get; set; }
        public int Nivel { get; set; }
        public string Fecha_Expira { get; set; }
        public string Clave_Cliente { get; set; }
        public string Nombre_Comercial { get; set; }
        public string Cadena { get; set; }
        public string Clave_unica { get; set; }
        public string Mail { get; set; }
        public string Status { get; set; }
    }
    
}
