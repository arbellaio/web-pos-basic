using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.EndOfDayReports;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.EndOfReports
{
    public interface IEndOfDayReportService
    {
        Task<List<EndOfDayReportSync>> GetEndOfDayReportsByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task<EndOfDayReportSync> GetEndOfDayReportById(int id);
        Task AddUpdateEndOfDayReport(List<EndOfDayReportSync> endOfDayReports);
    }

    public class EndOfDayReportService : IEndOfDayReportService
    {
        private readonly ApplicationDataContext _context;
        public EndOfDayReportService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<EndOfDayReportSync>> GetEndOfDayReportsByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
                return await _context.EndOfDayReports.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime)
                    .ToListAsync();
            }

            return null;
        }

        public async Task<EndOfDayReportSync> GetEndOfDayReportById(int id)
        {
            if (id > 0)
            {
                return await _context.EndOfDayReports.AsNoTracking().FirstOrDefaultAsync(x => x.EndOfDayReportId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateEndOfDayReport(List<EndOfDayReportSync> endOfDayReports)
        {
            if (endOfDayReports != null && endOfDayReports.Any())
            {
                foreach (var endOfDayReport in endOfDayReports)
                {
                    var endOfDayReportInDb = await GetEndOfDayReportById(endOfDayReport.EndOfDayReportId);
                    if (endOfDayReportInDb == null)
                    {
                        await _context.EndOfDayReports.AddAsync(endOfDayReport);
                    }
                    else
                    {
                        _context.EndOfDayReports.Update(endOfDayReport);
                    }

                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
