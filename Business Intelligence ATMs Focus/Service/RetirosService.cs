using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class RetirosService
    {
        private static RetirosService instancia = null;

        public static RetirosService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RetirosService();
                }

                return instancia;
            }
        }
        public Retiros Listar(int Clave_Sucursal)
        {
            SqlCommand cmd = null;
            Retiros Lista = null;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_InfoSucursalRetiros", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista = new Retiros
                            {
                                Fecha = dr["Fecha"].ToString(),
                                Hora = dr["Hora_Inicio"].ToString(),
                                Impote = dr["Importe_Total"].ToString(),
                                Retreat_today = (int)dr["Retreat_today"]
                            };

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new Retiros();
                }
            }
            return Lista;

        }


        public List<Retiros> Listar_Todos(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin)
        {
            SqlCommand cmd = null;
            List<Retiros> Lista = new List<Retiros>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Retiros", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    Conexion.creaParametro(cmd, "@Desde", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Inicio));
                    Conexion.creaParametro(cmd, "@Hasta", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Fin));
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Retiros()
                            {
                                Id_Retiro = Convert.ToInt32(dr["Folio"].ToString()),
                                Fecha = dr["Fecha"].ToString(),
                                Nombre_Sucursal = dr["NombreSucursal"].ToString(),
                                Hora = dr["Hora"].ToString(),
                                Impote = dr["Importe"].ToString(),
                                Usuario_RegistroRetiro = dr["Usuario"].ToString(),
                                Numero_Envase = dr["Remision"].ToString(),
                                Numero_Remision = dr["Envase"].ToString(),
                                Status = dr["Estatus"].ToString(),

                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Retiros>();
                }
            }
            return Lista;

        }
    }
}
