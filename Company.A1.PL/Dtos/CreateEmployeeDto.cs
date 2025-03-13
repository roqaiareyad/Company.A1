using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = " Name is required !")]
        public string Name { get; set; }

        [Range(18 , 60 , ErrorMessage =" Age Must be Between 18 and 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="Email Is Not Valid !")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{1,5}-[a-zA-Z]+-[a-zA-Z]+-[a-zA-Z]+$" ,ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsADeleted { get; set; }

        public DateTime CreateAt { get; set; }

        [DisplayName("Date of Create")]
        public DateTime HiringDate { get; set; }

    }
}
