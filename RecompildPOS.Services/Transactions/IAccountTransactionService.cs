using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Transactions;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Transactions
{
    public interface IAccountTransactionService
    {
        Task<List<AccountTransactionSync>> GetAccountTransactionByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task<AccountTransactionSync> GetAccountTransactionById(int id);
        Task AddUpdateAccountTransactions(List<AccountTransactionSync> accountTransactions);
    }

    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly ApplicationDataContext _context;
        public AccountTransactionService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<AccountTransactionSync>> GetAccountTransactionByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
                return await _context.AccountTransactions.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime).ToListAsync();
            }

            return null;
        }

        public async Task<AccountTransactionSync> GetAccountTransactionById(int id)
        {
            if (id > 0)
            {
                return await _context.AccountTransactions.AsNoTracking().FirstOrDefaultAsync(x => x.AccountTransactionId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateAccountTransactions(List<AccountTransactionSync> accountTransactions)
        {
            if (accountTransactions != null && accountTransactions.Any())
            {
                foreach (var accountTransaction in accountTransactions)
                {
                    accountTransaction.LastModifiedDate = DateTime.UtcNow;

                    var accountTransactionInDb =
                        await GetAccountTransactionById(accountTransaction.AccountTransactionId);

                    if (accountTransactionInDb == null)
                    {
                        await _context.AccountTransactions.AddAsync(accountTransaction);
                    }
                    else
                    {
                        _context.AccountTransactions.Update(accountTransaction);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
