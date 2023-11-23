using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class ConfiguracionServices
    {
        private static ConfiguracionServices instancia = null;

        public static ConfiguracionServices Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ConfiguracionServices();
                }

                return instancia;
            }
        }

        public List<Configuracion_Cajero> Get_Configuracion(string Clave_Sucursal)
        {
            List<Configuracion_Cajero> configuracion = new List<Configuracion_Cajero>();
            SqlCommand cmd = null;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_ConfiguracionCajero", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Sucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    cmd.Connection.Open();
                    List<Models.Validador> validadorList = new List<Models.Validador>();
                    List<Caset> casetList = new List<Caset>();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            if (Convert.ToInt32(dr["pNumValidadores"]) == 2)
                            {
                                //validador 1
                                validadorList.Add(new Models.Validador()
                                {
                                    Activo = Convert.ToBoolean(dr["pActivarVal1"]),
                                    Serie = dr["pSerieVal1"].ToString()
                                });
                                //caset 1
                                casetList.Add(new Caset()
                                {
                                    Serie = dr["pSerieCaset1"].ToString(),
                                    LimiteCapacidad = Convert.ToInt32(dr["pLimiteCapacidadKct"]),
                                    Capacidad = Convert.ToInt32(dr["pCapacidad_Caset"]),
                                    Cantidad_Actual = Convert.ToInt32(dr["pCapacidad_Actual"])
                                });
                                //validador 2
                                validadorList.Add(new Models.Validador()
                                {
                                    Activo = Convert.ToBoolean(dr["pActivarVal2"]),
                                    Serie = dr["pSerieVal2"].ToString()
                                });
                                //caset 2
                                casetList.Add(new Caset()
                                {
                                    Serie = dr["pSerieCaset2"].ToString(),
                                    LimiteCapacidad = Convert.ToInt32(dr["pLimiteCapacidadKct2"]),
                                    Capacidad = Convert.ToInt32(dr["pCapacidad_Caset2"]),
                                    Cantidad_Actual = Convert.ToInt32(dr["pCapacidad_Actual2"])
                                });
                            }
                            else
                            {

                                //validador 1
                                validadorList.Add(new Models.Validador()
                                {
                                    Activo = Convert.ToBoolean(dr["pActivarVal1"]),
                                    Serie = dr["pSerieVal1"].ToString()
                                });
                                //caset 1
                                casetList.Add(new Caset()
                                {
                                    Serie = dr["pSerieCaset1"].ToString(),
                                    LimiteCapacidad = Convert.ToInt32(dr["pLimiteCapacidadKct"]),
                                    Capacidad = Convert.ToInt32(dr["pCapacidad_Caset"]),
                                    Cantidad_Actual = Convert.ToInt32(dr["pCapacidad_Actual"])
                                });
                            }

                            configuracion.Add(new Configuracion_Cajero()
                            {
                                Plaza = GetPlaza(Convert.ToInt32(dr["Plaza"])),
                                RemisionDigital = Convert.ToBoolean(dr["RemisionDigital"]),
                                ManejaDepositoManual = Convert.ToBoolean(dr["pManejaDepositoManual"]),
                                ManejaFolioManual = Convert.ToBoolean(dr["pManejaFolioManual"]),
                                VerificaFolio = Convert.ToBoolean(dr["pvalidarFolio"]),
                                Numero_Validadores = Convert.ToInt32(dr["pNumValidadores"].ToString()),
                                Validadores = validadorList,
                                Casets = casetList,
                                Info = Get_InformacionPC(Clave_Sucursal)

                            });

                        }

                        if (!dr.HasRows)
                        {

                            configuracion.Add(new Configuracion_Cajero()
                            {
                                Plaza = "ERROR",
                                ManejaDepositoManual = false,
                                ManejaFolioManual = false,
                                RemisionDigital = false,
                                VerificaFolio = false,
                                Numero_Validadores = 0,
                                Validadores = validadorList,
                                Casets = casetList,
                                Info = Get_InformacionPC(Clave_Sucursal)

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    configuracion.Add(new Configuracion_Cajero()
                    {
                        Plaza = "ERROR",
                        ManejaDepositoManual = false,
                        ManejaFolioManual = false,
                        RemisionDigital = false,
                        VerificaFolio = false,
                        Numero_Validadores = 0,
                        Validadores = new List<Models.Validador>() { new Models.Validador() { Activo = false, Serie = "N/A" } },
                        Casets = new List<Caset>() { new Caset() { Serie = "N/A", LimiteCapacidad = 0, Capacidad = 0, Cantidad_Actual = 0 } },
                        Info = null

                    });
                }
            }

            return configuracion;
        }

        private string GetPlaza(int plaza)
        {
            string _plaza = "";
            if (plaza == 1) { _plaza = "MONTERREY"; }
            else if (plaza == 2) { _plaza = "SALTILLO"; }
            else if (plaza == 3) { _plaza = "DEVELOPER-MTY"; }

            return _plaza;
        }
        public List<Infor_PC> Get_InformacionPC(string Clave_Sucursal)
        {
            List<Infor_PC> informacion = new List<Infor_PC>();
            SqlCommand cmd = null;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Consultar_Pc_Config", oConexion);
                    Conexion.creaParametro(cmd, "@ClaveSucursal", System.Data.SqlDbType.VarChar, Clave_Sucursal);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            informacion.Add(new Infor_PC()
                            {
                                SO = dr["SO"].ToString(),
                                DD = dr["Unidad"].ToString() + "/",
                                RAM = dr["Ram_Tam"].ToString() + "GB ",
                                Procesador = dr["Procesador"].ToString(),
                                ProcesadorStatus = dr["Procesador_Status"].ToString(),
                                Procesador_Nucleos = dr["Procesador_Nuc"].ToString()

                            });


                        }
                        if (!dr.HasRows)
                        {
                            informacion.Add(new Infor_PC()
                            {
                                SO = "--",
                                DD = "--",
                                RAM = "--",
                                Procesador = "--",
                                ProcesadorStatus = "--",
                                Procesador_Nucleos = "--"
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    informacion.Add(new Infor_PC()
                    {
                        SO = "--",
                        DD = "--",
                        RAM = "--",
                        Procesador = "--",
                        ProcesadorStatus = "--",
                        Procesador_Nucleos = "--"

                    });

                }
            }

            return informacion;
        }


    }
}
