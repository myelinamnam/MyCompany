using System.Data;
using MyCompany.Handler.SqlInterface;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyCompany.Handler.SqlHelper
{
    public class AppDatabaseConnection : IDapperConnectionRepository
    {
        private readonly string _databaseConnectionString;
        public IDbConnection Database
        {
            get
            {
                return new SqlConnection(_databaseConnectionString);
            }
        }

        public AppDatabaseConnection(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("MyCompanyDb");
        }

    }
}
