using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AppleStore.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(IAccountRepository accountRepository, IConfiguration configuration, IEmailSender emailSender)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [BindProperty]
        public string Email { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmail(Email);
                if (user != null)
                {
                    var otp = new Random().Next(100000, 999999).ToString();
                    HttpContext.Session.SetString("OTP", otp);
                    HttpContext.Session.SetString("Email", Email);

                    // Gửi email OTP
                    var emailSent = await SendOtpEmailAsync(Email, otp);
                    if (emailSent)
                    {
                        SuccessMessage = "OTP đã được gửi đến email của bạn.";
                        return RedirectToPage("/Client/ConfirmOtp");
                    }
                    else
                    {
                        ErrorMessage = "Có lỗi xảy ra khi gửi email. Vui lòng thử lại sau.";
                    }
                }
                else
                {
                    ErrorMessage = "Không tìm thấy tài khoản với email này.";
                }
            }

            return Page();
        }

        private async Task<bool> SendOtpEmailAsync(string email, string otp)
        {
            try
            {
                var subject = "OTP để đặt lại mật khẩu của bạn";
                var body = $"OTP của bạn là: {otp}";

                await _emailSender.SendEmailAsync(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email OTP: {ex.Message}");
                return false;
            }
        }
    }
}
