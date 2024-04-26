using MyCompany.Core.Models;

namespace MyCompany.Handler.SqlInterface
{
    public interface ISqlHelperRepository
    {
        Task<GetAccessibleCompaniesResponse> GetAccessibleCompanies(int UserId);
        Task<BaseResponse> SaveCompany(SaveCompanyRequest request);
        void SaveLogs(SaveLogsRequest request);
        Task<BaseResponse> UpdateCompany(UpdateCompanyRequest request, int companyId);
        Task<BaseResponse> UpdateUserInformation(UpdateUserInfoRequest request, int userId);
        Task<BaseResponse> DeleteCompany(int CompanyId);
    }
}
