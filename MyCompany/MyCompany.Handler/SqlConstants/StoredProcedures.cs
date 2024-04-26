using System.Text;
using MyCompany.Core.Models;

namespace MyCompany.Handler.SqlConstants
{
    public class StoredProcedures
    {
        #region Stored Procedures
        public const string uspGetAccessibleCompanies = @"GetAccessibleCompanies";
        public const string uspSaveCompany = @"SaveCompany";
        public const string uspDeleteCompany = @"DeleteCompany";
        public const string uspUpdateCompany = @"UpdateCompany";
        public const string uspUpdateUserInformation = @"UpdateUserInformation";
        #endregion


        #region Select

        public static string SpGetAccessibleCompanies(int userId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($@"SELECT * FROM [dbo].[tblUserCompanyAccess] A WITH(NOLOCK)
                                       LEFT JOIN [dbo].[tblCompany] B ON A.CompanyId = B.Id
                                       WHERE A.UserId = {userId}");
            return queryBuilder.ToString();
        }

        #endregion




        #region Insert

        public static string SpInsertLogs(SaveLogsRequest request)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($@"INSERT INTO [dbo].[tblLogs] 
                                                   ([Endpoint] ,
                                                    [Request] ,
                                                    [Response] ,
                                                    [DateCreated])
                                       VALUES ('{request.Endpoint}', 
                                               '{request.Request}', 
                                               '{request.Response}',
                                               GETDATE())");
            return queryBuilder.ToString();
        }

        #endregion

    }
}
