using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service.Implements
{
    public class ServCustomer : IGenericService<CustomerModelo>
    {
        public async Task<bool> Editar(CustomerModelo modelo)
        {
            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("sprocedure_upadte_customers", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@customer_id", SqlDbType.VarChar, modelo.customer_id);
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

        public async Task<bool> Guardar(CustomerModelo modelo)
        {

            var razonsocial = Lista2().Result;
          var r =  razonsocial.Where(x=>x.rfc == modelo.rfc).ToList();

            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("sprocedure_insert_custumer", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@ownbranche_id", SqlDbType.Int, modelo.ownbranche_id);
                Conexion.creaParametro(cmd, "@social_reason_id", SqlDbType.Int, r[0].social_reason_id);
                Conexion.creaParametro(cmd, "@client_name", SqlDbType.VarChar, modelo.client_name);
                Conexion.creaParametro(cmd, "@client_address", SqlDbType.VarChar, modelo.client_address);
                Conexion.creaParametro(cmd, "@siac_key", SqlDbType.VarChar, modelo.siac_key);
                Conexion.creaParametro(cmd, "@status", SqlDbType.VarChar, modelo.status);
                Conexion.creaParametro(cmd, "@own_client_id", SqlDbType.VarChar, modelo.own_client_id);


                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<List<CustomerModelo>> Lista()
        {
            SqlCommand cmd = null;
            List<CustomerModelo> _lista = new List<CustomerModelo>();

            using (SqlConnection oConexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                try
                {

                    cmd = Conexion.creaComando("sprocedure_select_custumer", oConexion);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new CustomerModelo()
                            {
                                customer_id = (int)(dr["customer_id"]),
                                ownbranche_id = (int)dr["ownbranche_id"],
                                social_reason_id =(int) dr["social_reason_id"],
                                rfc = dr["rfc"].ToString(),
                                branch_name = Convert.ToString(dr["branch_name"].ToString()),
                                business_name = dr["business_name"].ToString(),
                                client_name = dr["client_name"].ToString(),
                                client_address = dr["client_address"].ToString(),
                                siac_key = dr["siac_key"].ToString(),
                                status = dr["status"].ToString(),
                                own_client_id = dr["own_client_id"].ToString()


                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _lista = new List<CustomerModelo>();
                }
            }
            return _lista;
        }

        public async Task<List<OwnBranchesModel>> Lista1()
        {
            SqlCommand cmd = null;
            List<OwnBranchesModel> _lista = new List<OwnBranchesModel>();

            using (SqlConnection oConexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                try
                {

                    cmd = Conexion.creaComando("sprocedure_select_ownbranches", oConexion);
                    Conexion.creaParametro(cmd, "@status", SqlDbType.VarChar, "A");
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new OwnBranchesModel()
                            {
                                ownbranche_id = (int)dr["key"],
                                branch_name = Convert.ToString(dr["name"].ToString()),
                                server_name = Convert.ToString(dr["server_name"].ToString()),
                                dba_name = Convert.ToString(dr["dba_name"].ToString()),
                                usr_name = Convert.ToString(dr["usr_name"].ToString()),
                                usr_password = Convert.ToString(dr["usr_password"].ToString())


                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _lista = new List<OwnBranchesModel>();
                }
            }
            return _lista;
        }


        public async Task<List<razonSocialModel>> Lista2()
        {
            SqlCommand cmd = null;
            List<razonSocialModel> _lista = new List<razonSocialModel>();

            using (SqlConnection oConexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                try
                {

                    cmd = Conexion.creaComando("sprocedure_select_customers", oConexion);

                    cmd.Connection.Open();
                    Conexion.creaParametro(cmd, "@status", SqlDbType.VarChar, "A");

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new razonSocialModel()
                            {
                                social_reason_id= (int)dr["social_reason_id"],
                                rfc = Convert.ToString(dr["rfc"].ToString()),
                                business_name = Convert.ToString(dr["business_name"].ToString()),


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


        public async  Task<List<CustomerModelo>> ListaBaseDedatos(OwnBranchesModel modelo)
        {
            SqlCommand cmd = null;
            List<CustomerModelo> _lista = new List<CustomerModelo>();


            var list = Lista1().Result;
            var select = list.Where(x => x.ownbranche_id == modelo.ownbranche_id).ToList();


            using (SqlConnection oConexion = new SqlConnection("Data Source=" + select[0].server_name + ";Initial Catalog=" + select[0].dba_name + ";User ID=" + select[0].usr_name + ";Password=" + select[0].usr_password + ""))
            {
                try
                {
                    cmd = Conexion.creaComando("sp_select_clientes", oConexion);
                    cmd.Connection.Open();
                    Conexion.creaParametro(cmd, "@rfc", SqlDbType.VarChar, modelo.rfc == null ? "T" : modelo.rfc);
                    Conexion.creaParametro(cmd, "@id_cliente", SqlDbType.VarChar, modelo.rs_id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new CustomerModelo()
                            {
                                ownbranche_id = Convert.ToInt32(dr["Id_Cliente"].ToString()),
                                siac_key = Convert.ToString(dr["Clave_Cliente"].ToString()),
                                //rfc = Convert.ToString(dr["rfc"].ToString()),
                                client_name = Convert.ToString(dr["Nombre_Comercial"].ToString()),
                                client_address = Convert.ToString(dr["Domicilio_Comercial"].ToString()),


                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _lista = new List<CustomerModelo>();
                }
            }
            return _lista;
        }
    }
}
