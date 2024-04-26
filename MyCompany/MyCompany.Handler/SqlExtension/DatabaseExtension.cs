using System.Data;

namespace MyCompany.Handler.SqlExtension
{
    public static class DatabaseExtension
    {
        public static IDbConnection TryOpen(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }
}
