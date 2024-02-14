using Business_Intelligence_ATMs_Focus.Service.Contrato;
using Business_Intelligence_ATMs_Focus.Models;
using System.Data;
using System.Data.SqlClient;

namespace Business_Intelligence_ATMs_Focus.Service.Implements
{
    public class SucursalesRepository : IGenericService<OwnBranchesModel>
    {
        public async Task<bool> Editar(OwnBranchesModel modelo)
        {

            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("Sprocedure_ownbranches_Update", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@ownbranche_id", SqlDbType.VarChar, modelo.ownbranche_id);
                Conexion.creaParametro(cmd, "@branch_name", SqlDbType.VarChar, modelo.branch_name);
                Conexion.creaParametro(cmd, "@server_name", SqlDbType.VarChar, modelo.server_name);
                Conexion.creaParametro(cmd, "@dba_name", SqlDbType.VarChar, modelo.dba_name);
                Conexion.creaParametro(cmd, "@usr_name", SqlDbType.VarChar, modelo.usr_name);
                Conexion.creaParametro(cmd, "@usr_password", SqlDbType.VarChar, modelo.usr_password);
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

        public async Task<bool> Guardar(OwnBranchesModel modelo)
        {
            SqlCommand cmd = null;
            using (var conexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                cmd = Conexion.creaComando("Sprocedure_ownbranches_Save", conexion);
                cmd.Connection.Open();
                Conexion.creaParametro(cmd, "@Sucursal", SqlDbType.VarChar, modelo.branch_name);
                Conexion.creaParametro(cmd, "@Servidor", SqlDbType.VarChar, modelo.server_name);
                Conexion.creaParametro(cmd, "@Dba", SqlDbType.VarChar, modelo.dba_name);
                Conexion.creaParametro(cmd, "@Usr", SqlDbType.VarChar, modelo.usr_name);
                Conexion.creaParametro(cmd, "@Password", SqlDbType.VarChar, modelo.usr_password);


                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<List<OwnBranchesModel>> Lista()
        {
            SqlCommand cmd = null;
            List<OwnBranchesModel> _lista = new List<OwnBranchesModel>();

            using (SqlConnection oConexion = new SqlConnection("Data Source=SQL-MTY-T01;Initial Catalog=CashFlowCenter;User ID=sa;Password=Siss@2020"))
            {
                try
                {
                    cmd = Conexion.creaComando("sprocedure_select_ownbranches", oConexion);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _lista.Add(new OwnBranchesModel()
                            {
                                ownbranche_id = Convert.ToInt32( dr["key"].ToString()),
                                branch_name = dr["name"].ToString(),
                                server_name = dr["server_name"].ToString(),
                                dba_name = dr["dba_name"].ToString(),
                                usr_name =dr["usr_name"].ToString(),
                                usr_password = dr["usr_password"].ToString(),
                                status = dr["status"].ToString()
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
    }
}
