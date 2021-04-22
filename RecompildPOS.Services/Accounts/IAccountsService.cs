using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Accounts;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Accounts
{
    public interface IAccountsService
    {
        Task<List<AccountSync>> GetAllAccountsByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task AddUpdateAccounts(List<AccountSync> accounts);
        Task<AccountSync> GetAccountById(int id);
    }

    public class AccountsService : IAccountsService
    {
        private readonly ApplicationDataContext _context;
        public AccountsService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("Db Context");
        }

        public async Task<List<AccountSync>> GetAllAccountsByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
               return await _context.Accounts.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime).ToListAsync();
            }

            return null;
        }

        public async Task AddUpdateAccounts(List<AccountSync> accounts)
        {
            if (accounts != null && accounts.Any())
            {
                foreach (var account in accounts)
                {
                    var accountInDb = await GetAccountById(account.AccountId);
                    if (accountInDb == null)
                    {
                        await _context.Accounts.AddAsync(account);
                    }
                    else
                    {
                        _context.Accounts.Update(account);
                    }

                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountSync> GetAccountById(int id)
        {
            if (id > 0)
            {
                return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.AccountId.Equals(id));
            }

            return null;
        }
    }
}
