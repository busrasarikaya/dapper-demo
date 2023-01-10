using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Helpers
{
    public class ConnectionHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateSqlConnection() => new SqlConnection(_connectionString);
    }
}


