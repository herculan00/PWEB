// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PWEB.Models;

namespace PWEB.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly UserManager<Utilizador> _userManager;
        private readonly IUserStore<Utilizador> _userStore;
        private readonly IUserEmailStore<Utilizador> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Utilizador> userManager,
            IUserStore<Utilizador> userStore,
            SignInManager<Utilizador> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            // Os atributos abaixo servem para completar a classe utilizador criada
            [Required]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Apelido")]
            public string Apelido { get; set; }

            [Required]
            [Display(Name = "NIF")]
            public int NIF { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Morada")]
            public string Morada { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "DataNascimento")]
            public DateTime DataNascimento { get; set; }

            [Required]
            [Display(Name = "NumeroCartaoMultibanco")]
            public int NumeroCartaoMultibanco { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "ValidadeCartaoMultibanco")]
            public DateTime ValidadeCartaoMultibanco { get; set; }

            [Required]
            [Display(Name = "CvdCartaoMultibanco")]
            public int CvdCartaoMultibanco { get; set; }

            [Required]
            [Display(Name = "Disponivel")]
            public bool Disponivel { get; set; }


            [DataType(DataType.DateTime)]
            [Display(Name = "Eliminar")]
            public DateTime? Eliminar { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                if (user.Nome != Input.Nome)
                {
                    user.Nome = Input.Nome;
                    await _userManager.UpdateAsync(user);
                }
                if (user.Apelido != Input.Apelido)
                {
                    user.Apelido = Input.Apelido;
                    await _userManager.UpdateAsync(user);
                }
                if (user.NIF != Input.NIF)
                {
                    user.NIF = Input.NIF;
                    await _userManager.UpdateAsync(user);
                }
                if (user.Morada != Input.Morada)
                {
                    user.Morada = Input.Morada;
                    await _userManager.UpdateAsync(user);
                }
                if (user.DataNascimento != Input.DataNascimento)
                {
                    user.DataNascimento = Input.DataNascimento;
                    await _userManager.UpdateAsync(user);
                }
                if (user.NumeroCartaoMultibanco != Input.NumeroCartaoMultibanco)
                {
                    user.NumeroCartaoMultibanco = Input.NumeroCartaoMultibanco;
                    await _userManager.UpdateAsync(user);
                }
                if (user.ValidadeCartaoMultibanco != Input.ValidadeCartaoMultibanco)
                {
                    user.ValidadeCartaoMultibanco = Input.ValidadeCartaoMultibanco;
                    await _userManager.UpdateAsync(user);
                }
                if (user.CvdCartaoMultibanco != Input.CvdCartaoMultibanco)
                {
                    user.CvdCartaoMultibanco = Input.CvdCartaoMultibanco;
                    await _userManager.UpdateAsync(user);
                }
                if (user.Disponivel != Input.Disponivel)
                {
                    user.Disponivel = Input.Disponivel;
                    await _userManager.UpdateAsync(user);
                }
                if (user.Eliminar != Input.Eliminar)
                {
                    user.Eliminar = Input.Eliminar;
                    await _userManager.UpdateAsync(user);
                }

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Ao criar conta passa a ser cliente
                    await _userManager.AddToRoleAsync(user, "Cliente");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Utilizador CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Utilizador>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Utilizador)}'. " +
                    $"Ensure that '{nameof(Utilizador)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Utilizador> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Utilizador>)_userStore;
        }
    }
}
