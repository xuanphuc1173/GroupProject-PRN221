using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AppleStore.Pages.Client
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public ChangePasswordModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [TempData]
        public string SuccessMessage { get; set; }
        [BindProperty]
        [Required]
        public string OldPassword { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be at least 6 characters long.")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Assuming the user ID is available; replace with actual user ID retrieval
            var userId = 1; // Replace with actual user ID retrieval logic
            var account = await _accountRepository.GetAccountById(userId);

            if (account == null || account.Password != OldPassword)
            {
                ModelState.AddModelError(string.Empty, "Invalid old password.");
                return Page();
            }

            account.Password = NewPassword;
            await _accountRepository.Update(account);
            SuccessMessage = "Change Password successful.";
            return RedirectToPage("/Index");
        }
    }
}
