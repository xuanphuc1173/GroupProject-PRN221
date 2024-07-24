using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BusinessObject;
using Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppleStore.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IAccountRepository accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [TempData]
        public string SuccessMessage { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.ValidateUser(UserName, Password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserType", user.Type);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetInt32("AccountId", user.AccountId);

                    TempData["SuccessMessage"] = "Logged in successfully.";
                    return RedirectToPage("/Index");
                }

                ErrorMessage = "Invalid login attempt.";
            }

            return Page();
        }
    }
}
