using MyCompany.Core.Models;

namespace MyCompany.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<BaseResponse> UpdateUserInformation(UpdateUserInfoRequest request, int UserId);
    }
}
