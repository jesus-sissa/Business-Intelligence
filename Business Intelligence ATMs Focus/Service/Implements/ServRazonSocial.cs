using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service.Implements
{
    public class ServRazonSocial : IGenericService<razonSocialModel>
    {
        public async Task<bool> Editar(razonSocialModel modelo)
        {
            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("sprocedure_update_social_reason", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@social_reason_id", SqlDbType.Int, modelo.social_reason_id);
                Conexion.creaParametro(cmd, "@rfc", SqlDbType.VarChar, modelo.rfc);
                Conexion.creaParametro(cmd, "@business_name", SqlDbType.VarChar, modelo.business_name);
                Conexion.creaParametro(cmd, "@businnes_address", SqlDbType.VarChar, modelo.businnes_address);
                Conexion.creaParametro(cmd, "@status", SqlDbType.VarChar, modelo.status);


                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Guardar(razonSocialModel modelo)
        {
            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("sprocedure_insert_sacial_reazon", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@rfc", SqlDbType.VarChar, modelo.rfc);
                Conexion.creaParametro(cmd, "@business_name", SqlDbType.VarChar, modelo.business_name);
                Conexion.creaParametro(cmd, "@businnes_address", SqlDbType.VarChar, modelo.businnes_address);
                Conexion.creaParametro(cmd, "@status", SqlDbType.VarChar, modelo.status);


                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }


        public async Task<List<razonSocialModel>> Lista()
        {

            SqlCommand cmd = null;
            List<razonSocialModel> _lista = new List<razonSocialModel>();

            using (SqlConnection oConexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                try
                {

                    cmd = Conexion.creaComando("sprocedure_select_customers", oConexion);

                    cmd.Connection.Open();
                    



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new razonSocialModel()
                            {
                                social_reason_id = (int)dr["social_reason_id"],
                                rfc = Convert.ToString(dr["rfc"].ToString()),
                                business_name = dr["business_name"].ToString(),
                                businnes_address = dr["businnes_address"].ToString(),
                                status = dr["status"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _lista = new List<razonSocialModel>();
                }
            }
            return _lista;


        }
    }
}

