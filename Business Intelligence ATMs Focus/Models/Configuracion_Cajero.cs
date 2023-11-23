namespace Business_Intelligence_ATMs_Focus.Models
{
    public class Configuracion_Cajero
    {
        
        public string Plaza { get; set; }
        public bool RemisionDigital { get; set; }
        public bool ManejaCorteDiario { get; set; }

        public bool ManejaDepositoManual { get; set; }

        public bool ManejaFolioManual { get; set; }

        public bool VerificaFolio { get; set; }
        public int Numero_Validadores { get; set; }

        public List<Validador> Validadores  { get; set; }

        public List<Caset> Casets { get; set; }

        public List<Infor_PC> Info { get; set; }
    }
   
}
public class Validador
{
    public bool Activo { get; set; }
    public string Serie { get; set; }
}

public class Caset 
{
   public string Serie { get; set; }
    public int LimiteCapacidad { get; set; }
	public int Capacidad { get; set; }
	public int Cantidad_Actual { get; set; }
}