using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppleStore.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public ResetPasswordModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.";
                    return Page();
                }

                var email = HttpContext.Session.GetString("Email");
                var user = await _accountRepository.GetUserByEmail(email);

                if (user != null)
                {
                    user.Password = Password; // Bạn có thể thêm mã hóa mật khẩu ở đây
                    await _accountRepository.Update(user);

                    return RedirectToPage("/Login");
                }

                ErrorMessage = "Đã xảy ra lỗi. Vui lòng thử lại.";
            }

            return Page();
        }
    }
}
