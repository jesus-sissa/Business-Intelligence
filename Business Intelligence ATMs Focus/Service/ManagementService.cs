using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class ManagementService
    {
        private static ManagementService instancia = null;

        public static ManagementService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ManagementService();
                }

                return instancia;
            }
        }

        public List<databases> GetDatabases()
        {
            SqlCommand cmd = null;
            List<databases> Lista = new List<databases>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_DataBases", oConexion);
                    //cmd.Notification = null;

                    //SqlDependency dependency = new SqlDependency(cmd);
                    //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new databases() { name = "[" + Tools.Decode(dr["name"].ToString()) + "]", ccliente = dr["CCLIENTE"].ToString(), alias = dr["Alias"].ToString(), connection = dr["connection"].ToString() });

                        }

                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<databases>();
                }
            }
            return Lista.ToList();


        }

        public List<databases> GetDatabasesPrb()
        {
            List<databases> Lista = new List<databases>();
            Lista.Add(new databases() { name = "[BANORTE]", ccliente = "0000", alias = "BANORTE", connection = "c2lzc2Euc291dGhjZW50cmFsdXMuY2xvdWRhcHAuYXp1cmUuY29t,QkFOT1JURQ==,c2lzc2E=,JDFzc0AyMDIw" });
            return Lista.ToList();

        }

        public bool ChangeConnection(string alias) 
        {
            bool resp = false;
            try
            {
                var nConexion = Data._databases.Where(x => x.alias == alias).FirstOrDefault();
                string[] Cadena = nConexion.connection.Split(',');
                Cadena[0] = Tools.Decode(Cadena[0]);
                Cadena[1] = Tools.Decode(Cadena[1]);
                Cadena[2] = Tools.Decode(Cadena[2]);
                Cadena[3] = Tools.Decode(Cadena[3]);
                Conexion.Sucursal = "Data Source=" + Cadena[0] + "; Initial Catalog=" + Cadena[1] + ";User ID=" + Cadena[2] + ";Password=" + Cadena[3] + ";";
                resp = true;
            }
            catch (Exception)
            {

               
            }
           
            return resp;
        }
        #region Usuarios
        public List<int> Info()
        {

            SqlCommand cmd = null;
            List<int> info = new List<int>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Inf_Users", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            info.Add(dr.GetInt32(0));
                            info.Add(dr.GetInt32(1));
                            info.Add(dr.GetInt32(2));
                        }
                        if (!dr.HasRows)
                        {
                            info.Add(0);
                            info.Add(0);
                            info.Add(0);
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return info;
            }
        }

        public List<Usuario> Get_Users()
        {
            SqlCommand cmd = null;
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Usrs", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            usuarios.Add(new Usuario()
                            {
                                Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                                Nombre_Sesion = dr["Nombre_Sesion"].ToString(),
                                Password = dr["Password"].ToString(),
                                Nivel = Convert.ToInt32(dr["Nivel"]),
                                Fecha_Expira = Convert.ToDateTime(dr["Fecha_Expira"]).ToShortDateString(),
                                Clave_unica = dr["CUNICA"].ToString(),
                                Status = dr["Status"].ToString()

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    usuarios = new List<Usuario>();
                }
            }

            return usuarios;
        }

        public Usuario Get_User(int id)
        {
            SqlCommand cmd = null;
            Usuario usuario = null;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Usr", oConexion);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            usuario = new Usuario()
                            {
                                Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                                Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                                Password = dr["Password"].ToString(),
                                Nombre_Sesion = dr["Nombre_Sesion"].ToString(),
                                Nivel = Convert.ToInt32(dr["Nivel"]),
                                Fecha_Expira = Convert.ToDateTime(dr["Fecha_Expira"]).ToShortDateString(),
                                Clave_Cliente = dr["CCLIENTE"].ToString(),
                                Nombre_Comercial = dr["NCOMERCIAL"].ToString(),
                                Cadena = dr["Cadena"].ToString(),
                                Clave_unica = dr["CUNICA"].ToString(),
                                Mail = dr["Mail"].ToString(),
                                Status = dr["Status"].ToString()

                            };
                        }
                    }

                }
                catch (Exception ex)
                {
                    usuario = new Usuario()
                    {
                        Id_Usuario = 0,
                        Nombre_Usuario = "--",
                        Password = "--",
                        Nombre_Sesion = "--",
                        Nivel = 0,
                        Fecha_Expira = Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                        Clave_Cliente = "--",
                        Nombre_Comercial = "--",
                        Cadena = "--",
                        Clave_unica = "--",
                        Mail = "--",
                        Status = "C"

                    };
                }
            }

            return usuario;
        }

        public bool Update_User(Usuario usuario)
        {
            bool resp = false;

            return resp;
        }

        public bool Escala_Grafica(string alias) 
        {
            bool _escala = false;
            SqlCommand cmd = null;
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Escalas", oConexion);
                    cmd.Parameters.AddWithValue("@alias", alias);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            Data.Escala_1 = dr["Escala_1"].ToString();
                            Data.Escala_2 = dr["Escala_2"].ToString();
                        }
                        _escala = true;
                    }

                }
                catch (Exception ex)
                {
                    usuarios = new List<Usuario>();
                }
            }

            return _escala;
        }

        #endregion

        #region Clientes
        public List<Sucursales> DrpDwn_Clientes()
        {
            SqlCommand cmd = null;
            List<Sucursales> clientes = new List<Sucursales>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Clientes", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clientes.Add(new Sucursales() { Clave_Sucursal = dr["CCLIENTE"].ToString(), Nombre_Sucursal = dr["NCOMERCIAL"].ToString() });
                        }
                        if (!dr.HasRows)
                        {
                            clientes.Add(new Sucursales() { Clave_Sucursal = "0", Nombre_Sucursal = "No Existe" });
                        }
                    }

                }
                catch (Exception ex)
                {
                    clientes.Add(new Sucursales() { Clave_Sucursal = "0", Nombre_Sucursal = "No Existe" });
                }
            }

            return clientes;
        }
        #endregion
        
    }
}
