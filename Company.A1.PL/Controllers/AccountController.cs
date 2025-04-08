﻿using Company.A1.DAL.Models;
using Company.A1.PL.Authentication;
using Company.A1.PL.Controllers;
using Company.A1.PL.Dtos;
using Company.A1.PL.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using NuGet.DependencyResolver;
using Twilio.TwiML.Voice;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Sms = Company.A1.PL.Helpers.Sms;

namespace Company.Route.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly ITwilioService _twilioService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService, ITwilioService twilioService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _twilioService = twilioService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(signUpDto.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(signUpDto.Email);
                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = signUpDto.UserName,
                            FirstName = signUpDto.FirstName,
                            LastName = signUpDto.LastName,
                            Email = signUpDto.Email,
                            IsAgree = signUpDto.IsAgree
                        };
                        var result = await _userManager.CreateAsync(user, signUpDto.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid Sign Up !!!!");

            }
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid Login !!");
            }
            return View();
        }
        [HttpGet]
        public new async Task<IActionResult> SignOut() // new because we want to hide the existing SignOut();
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
                    var email = new Email
                    {
                        To = model.Email,
                        Subject = "Password Reset",
                        Body = url!
                    };
                    _mailService.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");


                }
            }
            ModelState.AddModelError("", "Invalid Data");
            return View("ForgetPassword");
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrlSms(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
                    var sms = new Sms
                    {
                        To = user.PhoneNumber!,
                        Body = url!
                    };
                    _twilioService.SendSms(sms);
                    return RedirectToAction("CheckYourPhone");


                }
            }
            ModelState.AddModelError("", "Invalid Data");
            return View("ForgetPassword");
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CheckYourPhone()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return BadRequest("Invalid Operation");
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Successfully Reset Password";
                        return RedirectToAction("SignIn");
                    }
                }
                ModelState.AddModelError("", "Invalid Reset Password");
            }
            return View();
        }

        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(
                    claim => new
                    {
                        claim.Type,
                        claim.Value,
                        claim.Issuer,
                        claim.OriginalIssuer
                    }
                );
            return RedirectToAction("Index", "Home");
        }
        public IActionResult FacebookLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")
            };
            return Challenge(prop, FacebookDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(
                    claim => new
                    {
                        claim.Type,
                        claim.Value,
                        claim.Issuer,
                        claim.OriginalIssuer
                    }
                );
            return RedirectToAction("Index", "Home");
        }
    }
}