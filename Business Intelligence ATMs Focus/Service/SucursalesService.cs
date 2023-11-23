using Business_Intelligence_ATMs_Focus.Models;
using System.Data;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class SucursalesService
    {
        private static SucursalesService instancia = null;

        public static SucursalesService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new SucursalesService();
                }

                return instancia;
            }
        }
        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    MyHub.Show();
        //}
        public List<Sucursales> Litar_Por_Status(string Status)
        {
            if (Status.Equals("Enlinea"))
            {
                return Listar().Where(x => x.Minutos_desconexion <= 10).ToList();
            }
            else
                return Listar().Where(x => x.Minutos_desconexion > 10).ToList();
        }
        public List<Sucursales> Listar()
        {
            SqlCommand cmd = null;
            List<Sucursales> Lista = new List<Sucursales>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_SucursalesPorCorporativo", oConexion);
                    //cmd.Notification = null;

                    //SqlDependency dependency = new SqlDependency(cmd);
                    //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Sucursales()
                            {
                                Clave_Sucursal = dr["Clave_Sucursal"].ToString(),
                                Nombre_Sucursal = dr["Nombre_Sucursal"].ToString(),
                                Status = dr["Status"].ToString(),
                                Minutos_desconexion = Convert.ToInt32(dr["Minutos"].ToString()),
                                Dias_SinDepositar = Convert.ToInt32(dr["Dias_SinDepositar"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Sucursales>();
                }
            }
            return Lista.ToList();


        }
        public List<Sucursales> Monitoreo_Horario()
        {
            List<Sucursales> Lista = new List<Sucursales>();
            using (var connection = new SqlConnection(Conexion.Sucursal))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"Select  pFecha_Actualizacion,S.Nombre_Sucursal,S.Clave_Sucursal,Cast((DATEDIFF(MINUTE,pFecha_Actualizacion, GETDATE())) As varchar) As Minutos From ConfiguracionCajeros  As C Join Sucursales As S On S.Clave_Sucursal=C.pCve_Sucursal", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    //command.Notification = null;

                    //SqlDependency dependency = new SqlDependency(command);
                    //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new Sucursales()
                        {
                            Clave_Sucursal = reader["Clave_Sucursal"].ToString(),
                            Nombre_Sucursal = reader["Nombre_Sucursal"].ToString(),
                            Minutos_desconexion = Convert.ToInt32(reader["Minutos"].ToString())
                        });
                    }

                }
            }
            return Lista;
        }

        public List<Sucursales> SucursalesXCorporativo1()
        {
            
            List<Sucursales> sucursales = new List<Sucursales>();
            List<databases> databases = Data._databases;
            foreach (var item in databases)
            {
                SqlCommand cmdint = null;
                using (SqlConnection oConexionbd = new SqlConnection(Conexion.Central))
                {
                    oConexionbd.Open();
                    try
                    {
                        cmdint = Conexion.creaComando("Get_SucursalesPorCorporativo1", oConexionbd);
                        Conexion.creaParametro(cmdint, "@Corporativo", System.Data.SqlDbType.VarChar, item.name.ToUpper());
                        using (SqlDataReader drdb = cmdint.ExecuteReader())
                        {
                            while (drdb.Read())
                            {
                                sucursales.Add(new Sucursales()
                                {
                                    Clave_Sucursal = drdb["Clave_Sucursal"].ToString(),
                                    Nombre_Sucursal = drdb["Nombre_Corto"].ToString(),
                                    Status = drdb["Status"].ToString(),
                                    Minutos_desconexion = Convert.ToInt32(drdb["Minutos"].ToString()),
                                    Dias_SinDepositar = Convert.ToInt32(drdb["Dias_SinDepositar"]),
                                    Alias = item.alias,
                                    NumVal = Convert.ToInt32(drdb["pNumValidadores"]),
                                    Val1 = Convert.ToBoolean(drdb["validador1"]),
                                    Val2 = Convert.ToBoolean(drdb["validador2"]),
                                    Logo = drdb["Logo"].ToString()
                                });
                            }
                        }

                    }
                    catch (Exception)
                    {


                    }
                }
            }


            return sucursales;

        }
        public List<Sucursales> SucursalesXCorporativo()
        {

            List<Sucursales> sucursales = new List<Sucursales>();
            List<databases> databases = Data._databases;
            foreach (var item in databases)
            {
                SqlCommand cmdint = null;
                using (SqlConnection oConexionbd = new SqlConnection(Conexion.Central))
                {
                    oConexionbd.Open();
                    try
                    {
                        cmdint = Conexion.creaComando("Get_SucursalesPorCorporativo", oConexionbd);
                        Conexion.creaParametro(cmdint, "@Corporativo", System.Data.SqlDbType.VarChar, item.name.ToUpper());
                        using (SqlDataReader drdb = cmdint.ExecuteReader())
                        {
                            while (drdb.Read())
                            {
                                sucursales.Add(new Sucursales()
                                {
                                    Clave_Sucursal = drdb["Clave_Sucursal"].ToString(),
                                    Nombre_Sucursal = drdb["Nombre_Corto"].ToString(),
                                    Status = drdb["Status"].ToString(),
                                    Minutos_desconexion = Convert.ToInt32(drdb["Minutos"].ToString()),
                                    Dias_SinDepositar = Convert.ToInt32(drdb["Dias_SinDepositar"]),
                                    Alias = item.alias
                                  
                                });
                            }
                        }

                    }
                    catch (Exception)
                    {


                    }
                }
            }


            return sucursales;

        }
        public List<Sucursales> SucursalesXCorporativo(string corporativo)
        {

            List<Sucursales> sucursales = new List<Sucursales>();
           var databases = Data._databases.Where(x=> x.name.ToUpper() == "["+corporativo+"]").FirstOrDefault();
            SqlCommand cmdint = null;
            using (SqlConnection oConexionbd = new SqlConnection(Conexion.Central))
            {
                oConexionbd.Open();
                try
                {
                    cmdint = Conexion.creaComando("Get_SucursalesPorCorporativo", oConexionbd);
                    Conexion.creaParametro(cmdint, "@Corporativo", System.Data.SqlDbType.VarChar, databases.name.ToUpper());
                    using (SqlDataReader drdb = cmdint.ExecuteReader())
                    {
                        while (drdb.Read())
                        {
                            sucursales.Add(new Sucursales()
                            {
                                Clave_Sucursal = drdb["Clave_Sucursal"].ToString(),
                                Nombre_Sucursal = drdb["Nombre_Corto"].ToString(),
                                Status = drdb["Status"].ToString(),
                                Minutos_desconexion = Convert.ToInt32(drdb["Minutos"].ToString()),
                                Dias_SinDepositar = Convert.ToInt32(drdb["Dias_SinDepositar"]),
                                Alias = databases.alias
                            });
                        }
                    }

                }
                catch (Exception)
                {


                }
            }

            return sucursales;

        }
        private string GetAlias(string alias) 
        {
            string _alias = "";
            switch (alias)
            {
                case "CRMTY01":
                    _alias = "Carnes Ramos";
                    break;
                case "LCMTY02":
                    _alias = "Little Caesar";
                    break;
                default:
                    _alias = "S/A";
                    break;
            }

            return _alias;
        }


    }
}
