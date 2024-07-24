using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.AccountManage
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteModel(IAccountRepository accountRepository)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountRepository.GetAccountById(id.Value);
            if (Account != null)
            {
                Account = account;
                await _accountRepository.Delete(Account.AccountId);
            }

            return RedirectToPage("./Index");
        }
    }
}
