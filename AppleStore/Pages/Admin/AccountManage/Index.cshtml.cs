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
    public class IndexModel : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IAccountRepository _accountRepository;
        public IndexModel(ILogger<Index> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public IList<Account> Account { get;set; } = default!;
        public async Task OnGetAsync()
        {
            int accountType = 2; 
            Account = (await _accountRepository.GetAccountsByType(accountType)).ToList();
        }
    }
}
