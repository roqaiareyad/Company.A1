using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Email Is Required !!!")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Confirm Is Required !!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
