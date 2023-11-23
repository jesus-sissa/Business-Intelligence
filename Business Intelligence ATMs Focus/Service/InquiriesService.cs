using Business_Intelligence_ATMs_Focus.Models;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service
{
    public class InquiriesService
    {

        private static InquiriesService instancia = null;

        public static InquiriesService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new InquiriesService();
                }

                return instancia;
            }
        }

        public List<Sucursales> DrpDwn_Sucursales()
        {
            SqlCommand cmd = null;
            List<Sucursales> clientes = new List<Sucursales>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Sucursal))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_Sucursales", oConexion);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clientes.Add(new Sucursales() { Clave_Sucursal = dr["Clave_Sucursal"].ToString(), Nombre_Sucursal = dr["Nombre_Sucursal"].ToString() });
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
    }
}
