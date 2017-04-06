using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace mvc.App_Start
{
    public class Db
    {


        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



        public static DataTable GetTable(string sql)
        {
            SqlConnection connection = GetConnection();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

            DataSet dset = new DataSet();
            adapter.Fill(dset);
            DataTable table = dset.Tables[0];
            return table;
        }

        public static SqlConnection GetConnection()
        {

            SqlConnection connection = new SqlConnection()
            {
                ConnectionString = ConnectionString
            };

            return connection;
        }

        public static int Execute(SqlCommand command)
        {
            SqlConnection connection = GetConnection();

            command.Connection = connection;

            connection.Open();
            int rowsCount = command.ExecuteNonQuery();
            connection.Close();
            return rowsCount;
        }

        public static int Execute(string sql)
        {
            var t = WebConfigurationManager.ConnectionStrings["DefaultConnection"];

            SqlConnection connection = GetConnection();

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            int rowsCount = command.ExecuteNonQuery();
            connection.Close();
            return rowsCount;
        }

    }


}