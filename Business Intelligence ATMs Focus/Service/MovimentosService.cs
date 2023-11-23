using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class MovimientosService
    {
        private static MovimientosService instancia = null;

        public static MovimientosService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new MovimientosService();
                }

                return instancia;
            }
        }
        public List<Movimientos> Listar_Todos(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin)
        {
            SqlCommand cmd = null;
            List<Movimientos> Lista = new List<Movimientos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Movimientos", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    Conexion.creaParametro(cmd, "@Fecha_Desde", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Inicio));
                    Conexion.creaParametro(cmd, "@Fecha_Hasta", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Fin));
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Movimientos()
                            {

                                Fecha = dr["Fecha"].ToString(),
                                Hora = dr["HoraInicio"].ToString(),
                                ImpoteDeposito = dr["Deposito"].ToString(),
                                ImpoteRetiro = dr["Retiro"].ToString(),
                                Usuario_Registro = dr["Nombre"].ToString()

                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Movimientos>();
                }
            }
            return Lista;

        }
    }
}
