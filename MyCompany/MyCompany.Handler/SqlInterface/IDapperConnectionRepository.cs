using System.Data;

namespace MyCompany.Handler.SqlInterface
{
    public interface IDapperConnectionRepository
    {
        IDbConnection Database { get; }
    }
}
