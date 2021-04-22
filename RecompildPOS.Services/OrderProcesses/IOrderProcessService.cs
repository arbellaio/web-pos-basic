using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.OrderProcesses;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.OrderProcesses
{
    public interface IOrderProcessService
    {
        Task<List<OrderProcessSync>> GetOrderProcessesByBusinessId(int id, DateTime lastModifiedDateTime);
        Task<OrderProcessSync> GetOrderProcessById(int id);
        Task<OrderProcessDetailSync> GetOrderProcessDetailById(int id);
        Task AddUpdateOrderProcesses(List<OrderProcessSync> orderProcesses);
    }

    public class OrderProcessService : IOrderProcessService
    {
        private readonly ApplicationDataContext _context;
        public OrderProcessService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<OrderProcessSync>> GetOrderProcessesByBusinessId(int id, DateTime lastModifiedDateTime)
        {
            if (id > 0)
            {
                return await _context.OrderProcesses.AsNoTracking().Where(x => x.BusinessId.Equals(id) && x.LastModifiedDate >= lastModifiedDateTime)
                    .ToListAsync();
            }

            return null;
        }

        public async Task<OrderProcessSync> GetOrderProcessById(int id)
        {
            if (id > 0)
            {
                return await _context.OrderProcesses.AsNoTracking().FirstOrDefaultAsync(x => x.OrderProcessId.Equals(id));
            }

            return null;
        }

        public async Task<OrderProcessDetailSync> GetOrderProcessDetailById(int id)
        {
            if (id > 0)
            {
                return await _context.OrderProcessDetails.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.OrderProcessDetailId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateOrderProcesses(List<OrderProcessSync> orderProcesses)
        {
            if (orderProcesses != null && orderProcesses.Any())
            {
                foreach (var orderProcess in orderProcesses)
                {
                    orderProcess.LastModifiedDate = DateTime.UtcNow;
                    var orderProcessInDb = await GetOrderProcessById(orderProcess.OrderProcessId);
                    if (orderProcessInDb == null)
                    {
                        await _context.OrderProcesses.AddAsync(orderProcess);
                    }
                    else
                    {
                        _context.OrderProcesses.Update(orderProcess);
                    }

                    if (orderProcess.OrderProcessDetails != null && orderProcess.OrderProcessDetails.Any())
                    {
                        foreach (var orderProcessDetail in orderProcess.OrderProcessDetails)
                        {
                            orderProcessDetail.LastModifiedDate = DateTime.UtcNow;

                            var orderProcessDetailInDb =
                                await GetOrderProcessDetailById(orderProcessDetail.OrderProcessDetailId);
                            if (orderProcessDetailInDb == null)
                            {
                                await _context.OrderProcessDetails.AddAsync(orderProcessDetail);
                            }
                            else
                            {
                                _context.OrderProcessDetails.Update(orderProcessDetail);
                            }
                        }
                    }
                    
                }

                await _context.SaveChangesAsync();
            }

        }
    }
}
