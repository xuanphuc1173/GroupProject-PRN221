using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO: SingletonBase<AccountDAO>
    {

        public async Task<IEnumerable<Account>> GetAccountAll()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<Account> GetAccountById(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);
            if (account == null) return null; return account;
        }               
        public async Task<Account> GetAccountByUserName(string UserName)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.UserName == UserName);
            if (account == null) return null; return account;
        }
        public async Task<IEnumerable<Account>> GetAccountsByType(int type)
        {
            return await _context.Accounts.Where(a => a.Type == type).ToListAsync();
        }
        public async Task<Account> ValidateUser(string UserName, string Password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.UserName == UserName && c.Password == Password);
            if (account == null) return null; return account;
        }
        public async Task Add(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Account account)
        {
            var existingItem = await GetAccountById(account.AccountId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(account);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var account = await GetAccountById(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Account> GetAccountByEmail(string email)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.UserName == email);
            return account;
        }
    }
}
