using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Order;
using RecompildPOS.Services.OrderProcesses;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserSyncService _userSyncService;

        public OrderApiController(IOrderService orderService, IUserSyncService userSyncService)
        {
            _orderService = orderService ?? throw new ArgumentNullException("orderService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.Orders)]
        public async Task<OrderSyncCollection> GetOrders(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo, ApiEndPoints.Orders);

                var orders = await _orderService.GetAllOrdersByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime, TableNames.Orders,
                    ApiEndPoints.Orders, terminalLogId, orders.Count());

                var ordersSyncCollection = new OrderSyncCollection()
                {
                    Orders = orders,
                    Count = orders.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                    TerminalLogId = terminalLogId,
                };
                return ordersSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostOrders)]
        public async Task<bool> PostOrders(OrderSyncCollection orderSyncCollection)
        {
            if (orderSyncCollection != null && orderSyncCollection.Orders != null &&
                orderSyncCollection.Orders.Any() && !string.IsNullOrEmpty(orderSyncCollection.SerialNo))
            {
              
                await _userSyncService.AddUpdateSyncLog(orderSyncCollection.BusinessId, orderSyncCollection.SerialNo,
                    DateTime.UtcNow, TableNames.Orders, ApiEndPoints.PostOrders, orderSyncCollection.TerminalLogId,
                    orderSyncCollection.Count);

                await _orderService.AddUpdateOrders(orderSyncCollection.Orders);

                return true;
            }

            return false;
        }
    }
}