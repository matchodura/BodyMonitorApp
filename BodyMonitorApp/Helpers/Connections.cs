using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    class Connections
    {
        //private static string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Database=BodyMonitorDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["Default"].ConnectionString; } }
    }
}


