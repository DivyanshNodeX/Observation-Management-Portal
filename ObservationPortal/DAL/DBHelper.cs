using System.Configuration;
using System.Data.SqlClient;

namespace ObservationPortal.DAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        }

        public static void LogAction(string userName, string moduleName, string actionPerformed)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = @"INSERT INTO AuditLog
                        (UserName, ModuleName, ActionPerformed)
                         VALUES
                        (@UserName, @ModuleName, @ActionPerformed)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@ModuleName", moduleName);
                cmd.Parameters.AddWithValue("@ActionPerformed", actionPerformed);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}