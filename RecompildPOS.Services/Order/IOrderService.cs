using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Orders;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Order
{
    public interface IOrderService
    {
        Task AddUpdateOrders(List<OrderSync> orders);
        Task<List<OrderSync>> GetAllOrdersByBusinessId(int id, DateTime lastModifiedDateTime);
        Task<OrderSync> GetOrderById(int id);
        Task<OrderDetailSync> GetOrderDetailById(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDataContext _context;
        public OrderService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public async Task AddUpdateOrders(List<OrderSync> orders)
        {
            if (orders != null && orders.Any())
            {
                foreach (var order in orders)
                {
                    order.LastModifiedDate = DateTime.UtcNow;
                    var orderInDb = await GetOrderById(order.OrderId);
                    if (orderInDb == null)
                    {
                        await _context.Orders.AddAsync(order);
                    }
                    else
                    {
                        _context.Orders.Update(order);
                    }

                    if (order.OrderDetails != null && order.OrderDetails.Any())
                    {
                        foreach (var orderDetail in order.OrderDetails)
                        {
                            orderDetail.LastModifiedDate = DateTime.UtcNow;

                            var orderDetailInDb = await GetOrderDetailById(orderDetail.OrderDetailId);
                            if (orderDetailInDb == null)
                            {
                                await _context.OrderDetails.AddAsync(orderDetail);
                            }
                            else
                            {
                                _context.OrderDetails.Update(orderDetail);
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderSync>> GetAllOrdersByBusinessId(int id, DateTime lastModifiedDateTime)
        {
            if (id > 0)
            {
                return await _context.Orders.AsNoTracking().Where(x => x.BusinessId.Equals(id) && x.LastModifiedDate >= lastModifiedDateTime).ToListAsync();
            }

            return null;
        }

        public async Task<OrderSync> GetOrderById(int id)
        {
            if (id > 0)
            {
               return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId.Equals(id));
            }

            return null;
        }

        public async Task<OrderDetailSync> GetOrderDetailById(int id)
        {
            if (id > 0)
            {
                return await _context.OrderDetails.AsNoTracking().FirstOrDefaultAsync(x => x.OrderDetailId.Equals(id));
            }

            return null;
        }
    }
}
