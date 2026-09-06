using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDeskWeb.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Informe o e-mail.")]
            [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Informe a senha.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Manter conectado")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(
            string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(
                    TempData["ErrorMessage"] as string))
            {
                ModelState.AddModelError(
                    string.Empty,
                    TempData["ErrorMessage"] as string
                    ?? string.Empty
                );
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(
                IdentityConstants.ExternalScheme
            );

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(
            string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result =
                await _signInManager.PasswordSignInAsync(
                    Input.Email,
                    Input.Password,
                    Input.RememberMe,
                    lockoutOnFailure: false
                );

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(
                string.Empty,
                "E-mail ou senha inválidos."
            );

            return Page();
        }
    }
}
