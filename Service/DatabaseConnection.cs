using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HospitalCRM.Service
{
    public class DatabaseConnection
    {
        private SqlConnection Connection;

        public DatabaseConnection() { 
            Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\IU Ratnayake\Desktop\ES Project\HospitalCRM\hospitalDB.mdf"";Integrated Security=True;Connect Timeout=30");
        }

        public SqlConnection GetConnection() {
            return Connection;
        }
    }
}
