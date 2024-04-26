using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MyCompany.Core.Models;
using MyCompany.Application.Interfaces;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database transactions.</param>
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="request">The request containing the updated user information.</param>
        /// <param name="UserId">The UserId request to update user information.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        [Route("update/UserInformation/{UserId:int}")]
        public async Task<BaseResponse> UpdateUserInformation([FromBody][Required] UpdateUserInfoRequest request, int UserId)
        {
            return await _unitOfWork.User.UpdateUserInformation(request, UserId);
        }
    }
}
