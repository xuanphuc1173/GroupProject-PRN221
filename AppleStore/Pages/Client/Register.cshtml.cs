using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using BusinessObject;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AppleStore.Pages.Client
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public RegisterModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "User name is required.")]
        [GmailDomain(ErrorMessage = "User name must end with @gmail.com.")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }
            if (!UserName.EndsWith("@gmail.com"))
            {
                ErrorMessage = "User name must end with @gmail.com.";
                return Page();
            }
            var existingUser = await _accountRepository.GetAccountByUserName(UserName);
            if (existingUser != null)
            {
                ErrorMessage = "User name already exists.";
                return Page();
            }

            var newUser = new Account
            {
                UserName = UserName,
                Password = Password,
                FullName = FullName,
                Type = 2 // Or the appropriate value for user type
            };

            await _accountRepository.Add(newUser);
            SuccessMessage = "Registration successful.";
            return RedirectToPage("/Login");
        }
    }
}
