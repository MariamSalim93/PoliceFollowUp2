using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlHelper
    {

        
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=PoliceFollowup;Integrated Security=True";
       
        public async Task<DataTable> GetDataTableAsync(string storedProcedureName)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                await cnn.OpenAsync();
                await Task.Run(() => da.Fill(ds));
                return ds.Tables[0];
            }
        }

        public async Task<DataTable> GetDataTableAsync(string storedProcedureName, IDbDataParameter[] parameters)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    await cnn.OpenAsync();
                    await Task.Run(() => da.Fill(ds));
                    return ds.Tables[0];
                }
            }
        }

        public async Task ExecuteNonQueryAsync(string storedProcedureName)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));
            }

            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExecuteNonQueryAsync(string storedProcedureName, IDbDataParameter[] parameters)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> ExecuteScalarAsync(string storedProcedureName, IDbDataParameter[] parameters)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                await cnn.OpenAsync();
                return await cmd.ExecuteScalarAsync();
            }
        }

    }
}

