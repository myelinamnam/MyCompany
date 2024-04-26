using System.ComponentModel.DataAnnotations;

namespace MyCompany.Core.Models
{
    public class UpdateCompanyRequest
    {
        /// <summary>
        /// Name of the company.
        /// </summary>
        [StringLength(300)]
        [Required(ErrorMessage = "Company Name is required")]
        public string Name { get; set; }


        /// <summary>
        /// Description of the company.
        /// </summary>
        [StringLength(300)]
        [Required(ErrorMessage = "Company Description name is required")]
        public string Description { get; set; }


        /// <summary>
        /// Year the company was founded.
        /// </summary
        [Required(ErrorMessage = "Company FoundedYear name is required")]
        public int FoundedYear { get; set; }


        /// <summary>
        /// Industry of the company.
        /// </summary>
        [StringLength(200)]
        [Required(ErrorMessage = "Company Industry name is required")]
        public string Industry { get; set; }


        /// <summary>
        /// Headquarters location of the company.
        /// </summary>
        [StringLength(200)]
        [Required(ErrorMessage = "Company Industry name is required")]
        public string Headquarters { get; set; }


        /// <summary>
        /// Website URL of the company.
        /// </summary>
        [StringLength(200)]
        [Required(ErrorMessage = "Company Website name is required")]
        public string Website { get; set; }


        /// <summary>
        /// Contact email address of the company.
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "Company Email name is required")]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }


        /// <summary>
        /// Contact phone number of the company.
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "Company Phone name is required")]
        public string Phone { get; set; }


        /// <summary>
        /// CEO or leader of the company.
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "Company CEO name is required")]
        public string CEO { get; set; }
    }
}
