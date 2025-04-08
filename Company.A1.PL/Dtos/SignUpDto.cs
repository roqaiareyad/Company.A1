﻿using System.ComponentModel.DataAnnotations;

namespace Company.A1.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "FirstName is Required !!")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is Required !!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "ConfirmPassword is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The Password Doesn't Match Compare Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}