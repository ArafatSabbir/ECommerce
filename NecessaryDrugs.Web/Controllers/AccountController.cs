using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NecessaryDrugs.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using NecessaryDrugs.Core.Services;
using Microsoft.Extensions.Configuration;

namespace NecessaryDrugs.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<NormalUser> _signInManager;
        private readonly UserManager<NormalUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        public AccountController(
            UserManager<NormalUser> userManager,
            SignInManager<NormalUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult> Login(string returnUrl=null)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home");
            }
            var model = new LoginModel();
            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                //    lockoutOnFailure: true);
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    _logger.LogInformation("User logged in.");
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    //else if (await _userManager.IsInRoleAsync(user, "Admin"))
                    //{
                    //    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    //}
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Register(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home");
            }
            var model = new RegisterModel();
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new NormalUser { UserName = model.Email, Email = model.Email, 
                    PhoneNumber=model.PhoneNumber,FirstName=model.FirstName,LastName=model.LastName,Adress=model.Adress };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Action(
                    //    "ConfirmEmail","Account", new { userId = user.Id, code = code},
                    //    protocol: Request.Scheme, Request.Host.ToString());

                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    //var mailsender = new MailSender(_config);
                    //mailsender.Send(new List<MailMessage>
                    //{
                    //    new MailMessage
                    //    {
                    //        Body=callbackUrl,
                    //        Receiver=model.Email,
                    //        Sender="abdullahfaruquearman@gmail.com",
                    //        SenderName="Arman",
                    //        Subject="Confirm your email"
                    //    }
                    //});

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToAction("Login","Account");
                    //}
                    //else
                    //{
                        
                    //}

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(model.ReturnUrl);


                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(model.ReturnUrl);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            string StatusMessage;
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return View();
        }
        
    }
}
