using System.Data;
using System.Data.SqlClient;

namespace mvc.App_Start
{
    public class Db
    {
        public static DataTable GetTable(string sql)
        {
            SqlConnection connection = new SqlConnection()
            {
                ConnectionString = @"Data Source=andvas.database.windows.net;Initial Catalog=start;Integrated Security=False;User ID=nAIlIAvMN6;Password=usiq2gdKm0;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

            DataSet dset = new DataSet();
            adapter.Fill(dset);
            DataTable table = dset.Tables[0];
            return table;
        }

    }
}