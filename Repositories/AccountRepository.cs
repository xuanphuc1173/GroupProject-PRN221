using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository: IAccountRepository
    {
        public async Task<IEnumerable<Account>> GetAccountAll()
        {
            return await AccountDAO.Instance.GetAccountAll();
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await AccountDAO.Instance.GetAccountById(id);
        }        
        public async Task<Account> GetAccountByUserName(string UserName)
        {
            return await AccountDAO.Instance.GetAccountByUserName(UserName);
        }
        public async Task<IEnumerable<Account>> GetAccountsByType(int type)
        {
            return await AccountDAO.Instance.GetAccountsByType(type);
        }
        public async Task<Account> ValidateUser(string UserName, string Password)
        {
            return await AccountDAO.Instance.ValidateUser(UserName, Password);
        }

        public async Task Add(Account account)
        {
            await AccountDAO.Instance.Add(account);
        }

        public async Task Update(Account account)
        {
            await AccountDAO.Instance.Update(account);
        }

        public async Task Delete(int id)
        {
            await AccountDAO.Instance.Delete(id);
        }

        public async Task<Account> GetUserByEmail(string email)
        {
            return await AccountDAO.Instance.GetAccountByEmail(email);
        }
    }
}
