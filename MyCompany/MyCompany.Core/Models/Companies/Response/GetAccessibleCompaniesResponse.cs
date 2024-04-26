namespace MyCompany.Core.Models
{
    public class GetAccessibleCompaniesResponse
    {
        public BaseResponse Status { get; set; }
        public List<GetAccessibleCompaniesData> Data { get; set; }
    }

    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FoundedYear { get; set; }
        public string Industry { get; set; }
        public string Headquarters { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CEO { get; set; }
    }

    public class GetAccessibleCompaniesData
    {
        /// <summary>
        /// Unique ID of the user.
        /// </summary>
        public int UserID { get; set; }


        /// <summary>
        /// Unique ID of the company.
        /// </summary>
        public int CompanyID { get; set; }


        /// <summary>
        /// Name of the company.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Description of the company.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Year the company was founded.
        /// </summary>
        public int FoundedYear { get; set; }


        /// <summary>
        /// Industry of the company.
        /// </summary>
        public string Industry { get; set; }


        /// <summary>
        /// Headquarters location of the company.
        /// </summary>
        public string Headquarters { get; set; }


        /// <summary>
        /// Website URL of the company.
        /// </summary>
        public string Website { get; set; }


        /// <summary>
        /// Contact email address of the company.
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Contact phone number of the company.
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// CEO or leader of the company.
        /// </summary>
        public string CEO { get; set; }
    }
}
