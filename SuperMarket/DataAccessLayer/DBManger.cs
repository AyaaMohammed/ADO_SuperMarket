using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer
{
    public class DBManger
    {
        static SqlConnection con;
        static DBManger()
        {
            string connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure correct path
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetConnectionString("con1");
            con = new SqlConnection(connectionString);
        }

        public static DataTable GetQueryResult(string cmdText, SqlParameter[] parameters = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while executing the query.", ex);
            }
        }

        public static int ExecuteNonQuery(string cmdText, SqlParameter[] parameters = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                int x = cmd.ExecuteNonQuery();
                return x;
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while executing the non-query command.", ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static T ExecuteScalar<T>(string cmdText, SqlParameter[] parameters = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                return default(T);
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while executing the scalar command.", ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}