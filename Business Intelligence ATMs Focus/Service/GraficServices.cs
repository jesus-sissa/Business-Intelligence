using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class GraficServices
    {
        private static GraficServices instancia = null;

        public static GraficServices Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GraficServices();
                }

                return instancia;
            }
        }

        public List<Depositos> UltimosSieteDias(int Clave_Sucursal)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Get_Ventas_UltimosSieteDias", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                               Impote = dr["Total"].ToString(),
                               Fecha = Convert.ToDateTime(dr["Fecha"].ToString()).ToShortDateString()
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

        public List<Depositos> InformacionActual(int Clave_Sucursal, string status)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Get_Info_Actual", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["flag"].ToString() == "Depositos")
                            {
                                Lista.Add(new Depositos()
                                {
                                    Deposit_today = dr["Depositos_Dia"].ToString(),
                                    Impote = dr["Total_Depositado"].ToString(),
                                    Status = dr["flag"].ToString()
                                });
                            }
                            else {
                                Lista.Add(new Depositos()
                                {
                                    Impote = "0",
                                    Deposit_today ="0",
                                    Status = "Depositos"
                                });
                            }
                            if (dr["flag"].ToString() == "Retiros")
                            {
                                Lista.Add(new Depositos()
                                {
                                    Impote = dr["Depositos_Dia"].ToString(),
                                    Deposit_today = dr["Total_Depositado"].ToString(),
                                    Status = dr["flag"].ToString()
                                });
                            }
                            else
                            {
                                Lista.Add(new Depositos()
                                {
                                    Impote = "0",
                                    Deposit_today = "0",
                                    Status = "Retiros"
                                });
                            }



                        }

                        if (!dr.HasRows)
                        {
                            Lista.Add(new Depositos()
                            {
                                Impote = "0",
                                Deposit_today = "0",
                                Status = "Retiros"
                            });
                            Lista.Add(new Depositos()
                            {
                                Impote = "0",
                                Deposit_today = "0",
                                Status = "Depositos"
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

        public List<Depositos> InformacionMensual(int Clave_Sucursal, string status)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Get_Info_Mensual", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    //cmd.Parameters.AddWithValue("@status", status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["flag"].ToString() == "Depositos")
                            {
                                Lista.Add(new Depositos()
                                {
                                    Deposit_today = dr["Depositos_Dia"].ToString(),
                                    Impote = dr["Total_Depositado"].ToString(),
                                    Status = dr["flag"].ToString()
                                });
                            }
                            else
                            {
                                Lista.Add(new Depositos()
                                {
                                    Impote = "0",
                                    Deposit_today = "0",
                                    Status = "Depositos"
                                });
                            }
                            if (dr["flag"].ToString() == "Retiros")
                            {
                                Lista.Add(new Depositos()
                                {
                                    Impote = dr["Depositos_Dia"].ToString(),
                                    Deposit_today = dr["Total_Depositado"].ToString(),
                                    Status = dr["flag"].ToString()
                                });
                            }
                            else
                            {
                                Lista.Add(new Depositos()
                                {
                                    Impote = "0",
                                    Deposit_today = "0",
                                    Status = "Retiros"
                                });
                            }



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

        public List<Depositos> Depositos_Hoy(int Clave_Sucursal, string status)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Depositos_Hoy", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Id_Deposito = Convert.ToInt32(dr["Id_Deposito"]),
                                Impote = dr["Total_MXP"].ToString(),
                                Hora_Fin = Convert.ToDateTime(dr["Hora_fin"].ToString()).ToShortTimeString(),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                        if (!dr.HasRows)
                        {
                            Lista.Add(new Depositos()
                            {
                                Id_Deposito = 0,
                                Impote = "0",
                                Hora_Fin = "00:00",
                                Clave_Sucursal = Clave_Sucursal
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
        public List<Depositos> Depositos_Dia_Mes(int Clave_Sucursal, string fecha)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    //var x = Convert.ToDateTime(fecha).ToString("yyyy/MM/dd");
                    cmd = Conexion.creaComando("Depositos_Dia_Mes", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    cmd.Parameters.AddWithValue("@fecha", Convert.ToDateTime(fecha).ToString("yyyy/MM/dd"));
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Id_Deposito = Convert.ToInt32(dr["Id_Deposito"]),
                                Impote = dr["Total_MXP"].ToString(),
                                Hora_Fin = Convert.ToDateTime(dr["Hora_fin"].ToString()).ToShortTimeString(),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                        if (!dr.HasRows)
                        {
                            Lista.Add(new Depositos()
                            {
                                Id_Deposito = 0,
                                Impote = "0",
                                Hora_Fin = "00:00",
                                Clave_Sucursal = Clave_Sucursal
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

        public List<Depositos> Depositos_Mensual(int Clave_Sucursal, string status)
        {
            SqlCommand cmd = null;
            List<Depositos> Lista = new List<Depositos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Depositos_Mensual", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    //cmd.Parameters.AddWithValue("@status", status);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Depositos()
                            {
                                Impote = dr["Total_MXP"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"].ToString()).ToShortDateString(),
                                Clave_Sucursal = Convert.ToInt32(dr["Clave_Sucursal"].ToString())
                            });

                        }
                        if (!dr.HasRows)
                        {
                            Lista.Add(new Depositos()
                            {
                                Impote = "0.00",
                                Fecha = "--",
                                Clave_Sucursal = Clave_Sucursal
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

        public List<DesgloseDeposito> Depositos_Desglose(int Clave_Sucursal,int id_deposito)
        {
            SqlCommand cmd = null;
            List<DesgloseDeposito> Lista = new List<DesgloseDeposito>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {
                    cmd = Conexion.creaComando("Deposito_Desglose", oConexion);
                    cmd.Parameters.AddWithValue("@Clave_Sucursal", Clave_Sucursal);
                    cmd.Parameters.AddWithValue("@Id_Deposito", id_deposito);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new DesgloseDeposito()
                            {
                               Cantidad = dr["Cantidad"].ToString(),
                               Denominacion= dr["Denominacion"].ToString()
                            });

                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<DesgloseDeposito>();
                }
            }


            return Lista;
        }

    }
}
