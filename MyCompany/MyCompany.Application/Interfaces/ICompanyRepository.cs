using MyCompany.Core.Models;

namespace MyCompany.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<GetAccessibleCompaniesResponse> GetAccessibleCompanies(int UserId);
        Task<BaseResponse> SaveCompany(SaveCompanyRequest request);
        Task<BaseResponse> UpdateCompany(UpdateCompanyRequest request, int CompanyId);
        Task<BaseResponse> DeleteCompany(int companyId);
    }
}
