using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = " Code is required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = " Name is required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = " CreatedDate is required !")]
        public DateTime CreatedDate { get; set; }
    }
}
