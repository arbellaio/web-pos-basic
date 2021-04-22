using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Expense;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Expense
{
    public interface IBusinessExpenseService
    {
        Task<List<BusinessExpenseSync>> GetBusinessExpensesByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task<BusinessExpenseSync> GetBusinessExpenseById(int id);
        Task AddUpdateBusinessExpenses(List<BusinessExpenseSync> businessExpenses);
    }

    public class BusinessExpenseService : IBusinessExpenseService
    {
        private readonly ApplicationDataContext _context;
        public BusinessExpenseService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<BusinessExpenseSync>> GetBusinessExpensesByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
               return await _context.BusinessExpenses.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime).ToListAsync();
            }

            return null;
        }

        public async Task<BusinessExpenseSync> GetBusinessExpenseById(int id)
        {
            if (id > 0 )
            {
                return await _context.BusinessExpenses.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BusinessExpenseId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateBusinessExpenses(List<BusinessExpenseSync> businessExpenses)
        {
            if (businessExpenses != null && businessExpenses.Any())
            {
                foreach (var businessExpense in businessExpenses)
                {
                    var businessExpenseInDb = await _context.BusinessExpenses.FirstOrDefaultAsync(x =>
                        x.BusinessExpenseId.Equals(businessExpense.BusinessExpenseId));

                    if (businessExpenseInDb == null)
                    {
                        await _context.BusinessExpenses.AddAsync(businessExpense);
                    }
                    else
                    {
                        _context.BusinessExpenses.Update(businessExpense);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
