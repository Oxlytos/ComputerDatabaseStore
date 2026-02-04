using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
   public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
    }
    public class SQLConnectionFactory : IConnectionFactory
    {
        private readonly string _connstring;
        public SQLConnectionFactory(string connstring)
        {
            if (string.IsNullOrEmpty(connstring))
            {
                throw new ArgumentNullException("No connection string!");
            }
            _connstring = connstring;
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connstring);
        }
    }
}
