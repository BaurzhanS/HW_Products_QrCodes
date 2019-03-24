using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace HW_KoreanShop
{
    public class ConnectionStringInAppConfig
    {
        public string GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("No connection string provided!");
      
            return connectionString;
        }
        public void OpenConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            sqlConnection.Open();
        }
    }
}
