using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Finances;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Finances
{
    public interface IBusinessFinanceService
    {
        Task<List<BusinessFinanceSync>> GetBusinessFinancesByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task<BusinessFinanceSync> GetBusinessFinanceById(int id);
        Task AddUpdateBusinessFinances(List<BusinessFinanceSync> businessFinances);
    }

    public class BusinessFinanceService : IBusinessFinanceService
    {
        private readonly ApplicationDataContext _context;
        public BusinessFinanceService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<BusinessFinanceSync>> GetBusinessFinancesByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
                return await _context.BusinessFinances.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime)
                    .ToListAsync();
            }

            return null;
        }

        public async Task<BusinessFinanceSync> GetBusinessFinanceById(int id)
        {
            if (id > 0)
            {
                return await _context.BusinessFinances.AsNoTracking().FirstOrDefaultAsync(x => x.BusinessFinanceId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateBusinessFinances(List<BusinessFinanceSync> businessFinances)
        {
            if (businessFinances != null && businessFinances.Any())
            {
                foreach (var businessFinance in businessFinances)
                {
                    var businessFinanceInDb = await GetBusinessFinanceById(businessFinance.BusinessFinanceId);
                    if (businessFinanceInDb == null)
                    {
                        await _context.BusinessFinances.AddAsync(businessFinance);
                    }
                    else
                    {
                        _context.BusinessFinances.Update(businessFinance);
                    }

                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
