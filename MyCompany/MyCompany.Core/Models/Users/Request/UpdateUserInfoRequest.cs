using System.ComponentModel.DataAnnotations;

namespace MyCompany.Core.Models
{
    public class UpdateUserInfoRequest
    {

        [StringLength(100)]
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string EmailAddress { get; set; }


        [StringLength(20)]
        [Required(ErrorMessage = "Mobile number is required")]
        public string MobileNumber { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; }

        
        [StringLength(50)]
        [Required(ErrorMessage = "Last name is required")]
        public string Lastname { get; set; }


        // YYYYMMDD
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Birthday is required")]
        public DateTime Birthday { get; set; }


        [StringLength(300)]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
