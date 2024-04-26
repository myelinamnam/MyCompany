using MyCompany.Application.Interfaces;
using MyCompany.Core.Models;
using MyCompany.Handler.SqlHelper;
using MyCompany.Handler.SqlInterface;
using System.ComponentModel.Design;

namespace MyCompany.Infrastructure.Repositories
{
    internal class CompanyRepository : ICompanyRepository
    {
        private readonly ISqlHelperRepository _sqlHelperRepository;
        public CompanyRepository(ISqlHelperRepository sqlHelperRepository)
        {
            _sqlHelperRepository = sqlHelperRepository;
        }

        public async Task<GetAccessibleCompaniesResponse> GetAccessibleCompanies(int UserId)
        {
            GetAccessibleCompaniesResponse getAccessibleCompaniesResponse = new();
            getAccessibleCompaniesResponse = await _sqlHelperRepository.GetAccessibleCompanies(UserId);
            return getAccessibleCompaniesResponse;
        }

        public async Task<BaseResponse> SaveCompany(SaveCompanyRequest request)
        {
            BaseResponse baseResponse = await _sqlHelperRepository.SaveCompany(request);
            return baseResponse;
        }

        public async Task<BaseResponse> UpdateCompany(UpdateCompanyRequest request, int CompanyId)
        {
            BaseResponse baseResponse = await _sqlHelperRepository.UpdateCompany(request, CompanyId);
            return baseResponse;
        }

        public async Task<BaseResponse> DeleteCompany(int CompanyId)
        {
            BaseResponse baseResponse = await _sqlHelperRepository.DeleteCompany(CompanyId);
            return baseResponse;
        }
    }
}
