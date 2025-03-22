using System.ComponentModel.DataAnnotations;
using Company.A1.DAL.Models;

namespace Company.A1.PL.Dtos
{
    public class CreateDepartmentDto: BaseEntity
    {
        [Required(ErrorMessage = " Code is required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = " Name is required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = " CreatedDate is required !")]
        public DateTime CreatedDate { get; set; }
       
    }
}
