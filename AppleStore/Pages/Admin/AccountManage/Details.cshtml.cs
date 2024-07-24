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
    public class DetailsModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        public DetailsModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

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
    }
}
