using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Application.Interfaces;
using MyCompany.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Controller for managing company-related operations.
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database transactions.</param>
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Retrieves a list of companies accessible to the specified user.
        /// </summary>
        /// <param name="UserId">The ID of the user for whom to retrieve accessible companies.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpGet]
        [Route("company/AccessibleCompanies/{UserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetAccessibleCompaniesResponse> AccessibleCompanies(int UserId)
        {
            return await _unitOfWork.Company.GetAccessibleCompanies(UserId);
        }



        /// <summary>
        /// Creates a new company for the specified user.
        /// </summary>
        /// <param name="request">The request containing information to save the new company.</param>
        /// <returns>The result of the update operation.</returns>

        [HttpPost]
        [Route("company")]
        public async Task<BaseResponse> SaveCompany([FromBody] SaveCompanyRequest request)
        {
            return await _unitOfWork.Company.SaveCompany(request);
        }



        /// <summary>
        /// Updates an existing company with the specified ID.
        /// </summary>
        /// <param name="CompanyId">The ID of the company to update.</param>
        /// <param name="request">The request containing updated information for the company.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        [Route("company/{CompanyId:int}")]
        public async Task<BaseResponse> UpdateCompany([FromBody] UpdateCompanyRequest request, int CompanyId)
        {
            return await _unitOfWork.Company.UpdateCompany(request, CompanyId);
        }



        /// <summary>
        /// Deletes a company with the specified ID.
        /// </summary>
        /// <param name="CompanyId">The ID of the company to delete.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpDelete]
        [Route("company/{CompanyId:int}")]
        public async Task<BaseResponse> DeleteCompany([Required] int CompanyId)
        {
            return await _unitOfWork.Company.DeleteCompany(CompanyId);
        }
    }
}
