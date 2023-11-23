using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class UsuariosService
    {
        private static UsuariosService instancia = null;

        public static UsuariosService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new UsuariosService();
                }

                return instancia;
            }
        }
        public List<Usuario> Listar()
        {
            SqlCommand cmd = null;
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Central))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_InformacionUsr", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
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

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                }
            }
            return Lista;
        }

        public bool Cambiar_password(int Id_usuario, string pass)
        {
            bool resp = false;

            using (SqlConnection connection = new SqlConnection(Conexion.Central))
            {
                connection.Open();
                try
                {

                    string PasswordHASH = Tools.EncriptacionSHA1(pass);
                    string query = "Update usrctes Set Password = @PasswordHASH, Fecha_Expira = Convert(date, DATEADD(YEAR, 1, GETDATE()), 23) Where Id_usuario = @Id_Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@PasswordHASH", PasswordHASH);
                        command.Parameters.AddWithValue("@Id_Usuario", Id_usuario);
                        command.ExecuteNonQuery();
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return resp;
        }
    }
}
