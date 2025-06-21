using FET_MVCforTest.Helper;
using FET_MVCforTest.Models;
using FET_MVCforTest.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FET_MVCforTest.Controllers
{



	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var UserName = await _userManager.FindByNameAsync(model.UserName);
			if (UserName is { })
			{
				ModelState.AddModelError(nameof(SignUpViewModel.UserName), "Sorry this user name is already used by another user !");
				return View(model);
			}

			var user = new ApplicationUser
			{
				UserName = model.UserName,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				IsAgree = model.IsAgree,
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
				return RedirectToAction(nameof(SignIn));

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}
		#endregion



		#region SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is { })
			{
				bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
				if (flag)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

					if (result.IsNotAllowed)
						ModelState.AddModelError(string.Empty, "This account is not confirmed !");

					if (result.IsLockedOut)
						ModelState.AddModelError(string.Empty, "This account is locked");

					if (result.Succeeded)
						return RedirectToAction(nameof(HomeController.Index), "Home");

				}
			}

			ModelState.AddModelError(string.Empty, "Invalid login attempt !");
			return View();

		}
		#endregion


		#region SignOut

		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion


		#region Forget Password

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
				if (user is { })
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { forgetPasswordViewModel.Email, token }, "https");

					var email = new Email()
					{

						Subject = "Reset your password",
						Body = ResetPasswordLink ?? "",
						To = user?.Email!
					};

					await H_SendEmail.SendEmail(email);

					return RedirectToAction(nameof(CheckYourInbox));
				}
			}

			ModelState.AddModelError(string.Empty, "Invalid email");
			return View(forgetPasswordViewModel);



		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}



		#endregion


		#region Reset Password

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
		{
			if (ModelState.IsValid)
			{

				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email!);

				var result = await _userManager.ResetPasswordAsync(user!, token!, resetPasswordViewModel.NewPassword);

				if (result.Succeeded)
					return RedirectToAction(nameof(SignIn));

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

			}
			return View(resetPasswordViewModel);

		}

        #endregion


        


    }
}
