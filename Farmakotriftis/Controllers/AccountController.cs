using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmakotriftis.Services;
using Farmakotriftis.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Farmakotriftis.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            string confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);
            //EmailService.Send(user.Email, "Confirm Your Email", "Click Here To Confirm your Email Address " + confirmationLink);
            System.IO.File.WriteAllText(@"C:\Temp\ConfirmEmail.txt", confirmationLink);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Msg = "Email confimation Succeeded!";
            }
            else
            {
                ViewBag.Msg = "Email confimation Failed!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var Result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
            if (Result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
                //var user = await userManager.FindByNameAsync(model.UserName);
                //if (user.EmailConfirmed)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    await signInManager.SignOutAsync();
                //}
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.UserEmail);
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            string resetLink = Url.Action("ResetPassword", "Account", new { userid = user.Id, token = resetToken }, protocol: HttpContext.Request.Scheme);
            //EmailService.Send(user.Email, "Confirm Your Email", "Click Here To Confirm your Email Address " + confirmationLink);
            System.IO.File.WriteAllText(@"C:\Temp\ResetPassword.txt", resetLink);
            //var callbackUrl = .ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
            //SendEmail
            ViewBag.Msg = "Reset Password Link Has Been Emailed";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            var obj = new ResetPasswordViewModel() { UserId = userId, Token = token };
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                ViewBag.Msg = "Password Reset Succeeded!";
            }
            else
            {
                ViewBag.Msg = "Password Rest Failed!";
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}