using MyCompany.Handler.SqlInterface;
using MyCompany.Core.Models;
using MyCompany.Handler.SqlExtension;
using MyCompany.Handler.SqlConstants;
using System.Data;
using Dapper;

namespace MyCompany.Handler.SqlHelper
{
    public class SQLHelper : ISqlHelperRepository
    {
        private readonly IDapperConnectionRepository _connection;
        public SQLHelper(IDapperConnectionRepository connection)
        {
            _connection = connection;
        }



        #region Select/Get

        public async Task<GetAccessibleCompaniesResponse> GetAccessibleCompanies(int UserId)
        {
            using var db = _connection.Database.TryOpen();
            List<GetAccessibleCompaniesData> getAccessibleCompaniesData = new();

            try
            {
                var result = db.Query<GetAccessibleCompaniesData, Company, GetAccessibleCompaniesData>(
                    sql: StoredProcedures.uspGetAccessibleCompanies,
                    map: (user, company) =>
                    {
                        user.CompanyID = company.CompanyID;
                        user.Name = company.Name;
                        user.Description = company.Description;
                        user.FoundedYear = company.FoundedYear;
                        user.Industry = company.Industry;
                        user.Headquarters = company.Headquarters;
                        user.Website = company.Website;
                        user.Email = company.Email;
                        user.Phone = company.Phone;
                        user.CEO = company.CEO;
                        return user;
                    },
                    param: new { UserId },
                    splitOn: "CompanyID",
                    commandType: CommandType.StoredProcedure,
                    buffered: true
                );


                var response = new GetAccessibleCompaniesResponse
                {
                    Status = new BaseResponse
                    {
                        StatusCode = 0,
                        StatusMessage = "Success"
                    },
                    Data = result.AsList()
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new GetAccessibleCompaniesResponse
                {
                    Status = new BaseResponse
                    {
                        StatusCode = 1,
                        StatusMessage = "User not found."
                    },
                    Data = getAccessibleCompaniesData.AsList()
                };
                Console.WriteLine(ex.Message);
                return response;
            }
        }
        #endregion







        #region Insert

        public void SaveLogs(SaveLogsRequest request)
        {
            try
            {
                using var db = _connection.Database.TryOpen();
                db.Execute(StoredProcedures.SpInsertLogs(request), new
                {
                    request.Endpoint,
                    request.Request,
                    request.Response
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




        public async Task<BaseResponse> SaveCompany(SaveCompanyRequest request)
        {
            try
            {
                using var db = _connection.Database.TryOpen();
                var insertResult = await db.QueryAsync<BaseResponse>(
                    StoredProcedures.uspSaveCompany, new
                    {
                        request.Name,
                        request.Description,
                        request.FoundedYear,
                        request.Industry,
                        request.Headquarters,
                        request.Website,
                        request.Email,
                        request.Phone,
                        request.CEO
                    }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                if (insertResult == null)
                    throw new Exception("No response from the stored procedure.");

                return insertResult.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion







        #region Update

        public async Task<BaseResponse> UpdateCompany(UpdateCompanyRequest request, int CompanyId)
        {
            try
            {
                using var db = _connection.Database.TryOpen();
                var result = await db.QueryAsync<BaseResponse>(
                   StoredProcedures.uspUpdateCompany, new
                   {
                       CompanyId,
                       request.Name,
                       request.Description,
                       request.FoundedYear,
                       request.Industry,
                       request.Headquarters,
                       request.Website,
                       request.Email,
                       request.Phone,
                       request.CEO
                   }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                
                if (result == null)
                    throw new Exception("No response from the stored procedure.");

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




        public async Task<BaseResponse> UpdateUserInformation(UpdateUserInfoRequest request, int UserId)
        {
            try
            {
                using var db = _connection.Database.TryOpen();
                var result = await db.QueryAsync<BaseResponse>(
                   StoredProcedures.uspUpdateUserInformation, new
                   {
                       UserId,
                       request.EmailAddress,
                       request.MobileNumber,
                       request.Firstname,
                       request.Lastname,
                       request.Birthday,
                       request.Address
                   }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                if (result == null)
                    throw new Exception("No response from the stored procedure.");

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion







        #region Delete

        public async Task<BaseResponse> DeleteCompany(int CompanyId)
        {
            try
            {
                using var db = _connection.Database.TryOpen();
                var result = await db.QueryAsync<BaseResponse>(StoredProcedures.uspDeleteCompany, new
                {
                    CompanyId
                },commandType: CommandType.StoredProcedure);
                if (result == null)
                    throw new Exception("No response from the stored procedure.");

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion

    }
}
