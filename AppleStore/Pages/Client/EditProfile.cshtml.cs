using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Repositories;
using System.IO;
using System.Threading.Tasks;
using System;

namespace AppleStore.Pages.Client
{
    public class EditProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditProfileModel(IAccountRepository accountRepository, IWebHostEnvironment webHostEnvironment)
        {
            _accountRepository = accountRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [TempData]
        public string SuccessMessage { get; set; }

        [BindProperty]
        public Account Account { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Account = await _accountRepository.GetAccountById(id);

            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool? deletePicture)
        {
            var existingAccount = await _accountRepository.GetAccountById(Account.AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.UserName = Account.UserName;
            existingAccount.FullName = Account.FullName;

            if (deletePicture.HasValue && deletePicture.Value)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(existingAccount.Picture))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingAccount.Picture.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    existingAccount.Picture = null; // Xóa đường dẫn ảnh
                }
            }
            else if (Picture != null)
            {
                // Lưu ảnh mới
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Picture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Picture.CopyToAsync(fileStream);
                }

                existingAccount.Picture = "/uploads/" + uniqueFileName;
            }

            await _accountRepository.Update(existingAccount);

            SuccessMessage = "Update Profile successful.";
            return RedirectToPage("/Index");
        }
    }
}
