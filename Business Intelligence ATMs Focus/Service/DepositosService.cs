using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class DepositosService
    {

        private static DepositosService instancia = null;

        public static DepositosService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DepositosService();
                }

                return instancia;
            }
        }
        public Depositos Listar(int Clave_Sucursal)
        {
            SqlCommand cmd = null;
            Depositos Lista = null;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_InfoSucursal", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista = new Depositos
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Fecha = dr["Fecha"].ToString(),
                                Hora = dr["Hora_Inicio"].ToString(),
                                Impote = dr["Importe_Total"].ToString(),
                                Clave_Sucursal = Clave_Sucursal,
                                Deposit_today = dr["Deposit_today"].ToString()

                            };

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new Depositos();
                }
            }
            return Lista;

        }
        public List<Depositos> Listar_Todos(string Clave_Sucursal, string Fecha_Inicio, string Fecha_Fin)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Depositos", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    Conexion.creaParametro(cmd, "@Fecha_Desde", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Inicio));
                    Conexion.creaParametro(cmd, "@Fecha_Hasta", System.Data.SqlDbType.Date, Convert.ToDateTime(Fecha_Fin));
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Fecha = dr["Fecha"].ToString(),
                                Nombre_Sucursal = dr["NombreSucursal"].ToString(),
                                Hora = dr["HoraInicio"].ToString(),
                                Hora_Fin = dr["HoraFin"].ToString(),
                                Impote = dr["Importe"].ToString(),
                                Usuario_Registro = dr["Usuario"].ToString(),
                                Status = dr["Estatus"].ToString(),

                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;

        }
        public List<Depositos> Listar_DepositosGeneral()
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            List<Sucursales> LSucursales = new List<Sucursales>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Get_DepositosGeneral", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Cantidad_Depositos_MesActual = Convert.ToDecimal(dr["actual"].ToString()),
                                Cantidad_Depositos_MesAnterior = Convert.ToDecimal(dr["anterior"].ToString()),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }


            return Lista;
        }
        public List<Sucursales> Monitoreo_sucursales()
        {
            SqlCommand cmd = null;
            List<Sucursales> Lista = new List<Sucursales>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Get_DepositosGeneral_Promedio", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Sucursales()
                            {
                                Total_Enlinea = (int)dr["Enlinea"],
                                Total_Desconectado = (int)dr["Desconectado"]
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Sucursales>();
                }
            }
            return Lista;

        }
        public List<Depositos> Listar_SucursalesTop()
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Sucursales_Top", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Impote = dr["Depositos"].ToString()
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;

        }


        public List<Depositos> Listar_SucursalesTopVentasDia()
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_TopSucursalesDia", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Impote = dr["Depositos"].ToString(),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;

        }

        public List<Depositos> Listar_Sucursales_TopDia(string status) 
        {
            
                 SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Sucursales_Dia", oConexion);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Impote = dr["Depositos"].ToString(),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;

        }

        public List<Depositos> ListaDepositosxSucursal(string sucursal)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("DepositosDiaxSucursal", oConexion);
                    cmd.Parameters.AddWithValue("@sucursal", sucursal);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Impote = dr["Deposito"].ToString()
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;
        }

        public List<Depositos> ListUltimosDepositosRetiros()
        {

            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("UltimosMovimientosCajeros", oConexion);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Fecha = dr["Fecha"].ToString(),

                                Impote = dr["Importe_Total"].ToString(),
                                Status = dr["Status"].ToString()
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;
        }

        public List<Depositos> ListUltimosDepositos()
        {

            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("UltimosMovimientosDepositos", oConexion);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Fecha = dr["Fecha"].ToString(),

                                Impote = dr["Importe_Total"].ToString(),
                                Status = dr["Status"].ToString()
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;
        }

        public List<Depositos> ListUltimosRetiros()
        {

            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("UltimosMovimientosRetiros", oConexion);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Fecha = dr["Fecha"].ToString(),

                                Impote = dr["Importe_Total"].ToString(),
                                Status = dr["Status"].ToString()
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Depositos>();
                }
            }
            return Lista;
        }


    }
}
