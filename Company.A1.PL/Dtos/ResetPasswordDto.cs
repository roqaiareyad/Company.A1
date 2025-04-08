using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "ConfirmPassword is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The Password Doesn't Match Compare Password")]
        public string ConfirmPassword { get; set; }
    }
}
