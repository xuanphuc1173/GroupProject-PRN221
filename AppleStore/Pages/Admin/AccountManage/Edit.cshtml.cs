using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.AccountManage
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public EditModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _accountRepository.GetAccountById(id.Value);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingAccount = await _accountRepository.GetAccountById(Account.AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính
            existingAccount.UserName = Account.UserName;
            existingAccount.Password = Account.Password;
            existingAccount.FullName = Account.FullName;
            existingAccount.Picture = Account.Picture;

            await _accountRepository.Update(existingAccount);

            return RedirectToPage("./Index");
        }
    }
}
