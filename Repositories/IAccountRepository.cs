using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountAll();
        Task<IEnumerable<Account>> GetAccountsByType(int type);
        Task<Account> GetAccountById(int id);
        Task<Account> GetAccountByUserName(string UserName);
        Task<Account> ValidateUser(string UserName, string Password);
        Task Add(Account account);
        Task Update(Account account);
        Task Delete(int id);
        Task<Account> GetUserByEmail(string email); // Phương thức cần thêm

    }
}
