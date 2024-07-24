using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppleStore.Pages
{
    public class ConfirmOtpModel : PageModel
    {
        [BindProperty]
        public string Otp { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var sessionOtp = HttpContext.Session.GetString("OTP");
                var sessionEmail = HttpContext.Session.GetString("Email");

                if (Otp == sessionOtp)
                {
                    return RedirectToPage("/Client/ResetPassword");
                }

                ErrorMessage = "OTP không hợp lệ.";
            }

            return Page();
        }
    }
}
