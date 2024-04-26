using MyCompany.Application.Interfaces;
using MyCompany.Core.Models;
using MyCompany.Handler.SqlInterface;
using System.ComponentModel.Design;

namespace MyCompany.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly ISqlHelperRepository _sqlHelperRepository;
        public UserRepository(ISqlHelperRepository sqlHelperRepository)
        {
            _sqlHelperRepository = sqlHelperRepository;
        }
        public async Task<BaseResponse> UpdateUserInformation(UpdateUserInfoRequest request, int UserId)
        {
            BaseResponse baseResponse = await _sqlHelperRepository.UpdateUserInformation(request, UserId);
            return baseResponse;
        }
    }
}
