using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName Is Required !!!") ]
        public string UserName { get; set; }



        [Required(ErrorMessage = "FirstName Is Required !!!")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "LastName Is Required !!!")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Email Is Required !!!")]
        [EmailAddress]
        public string Email { get; set; }



        [Required(ErrorMessage = "Confirm Is Required !!!")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }




        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is Required !!!")]
        [Compare(nameof(Password),ErrorMessage = " Confirm Password doesn't Match the Password !!" )]
        public string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }
    }
}
