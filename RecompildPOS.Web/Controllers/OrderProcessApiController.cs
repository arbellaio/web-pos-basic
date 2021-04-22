using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.OrderProcesses;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessApiController : ControllerBase
    {
        private readonly IOrderProcessService _orderProcessService;
        private readonly IUserSyncService _userSyncService;

        public OrderProcessApiController(IOrderProcessService orderProcessService, IUserSyncService userSyncService)
        {
            _orderProcessService = orderProcessService ?? throw new ArgumentNullException("orderProcessService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.OrderProcesses)]
        public async Task<OrderProcessSyncCollection> GetOrderProcesses(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo,
                        ApiEndPoints.OrderProcesses);

                var syncLog = new UserSyncLog
                {
                    SerialNo = serialNo,
                    BusinessId = businessId,
                    LastUpdatedDate = requestedDateTime,
                    ApiEndPoint = ApiEndPoints.OrderProcesses,
                    TableName = TableNames.OrderProcesses,
                };

                var orderProcesses = await _orderProcessService.GetOrderProcessesByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, DateTime.UtcNow,
                    TableNames.OrderProcesses, ApiEndPoints.OrderProcesses, terminalLogId, orderProcesses.Count());

                var orderProcessSyncCollection = new OrderProcessSyncCollection()
                {
                    OrderProcesses = orderProcesses,
                    Count = orderProcesses.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                    TerminalLogId = terminalLogId,
                };
                return orderProcessSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostOrderProcesses)]
        public async Task<bool> PostOrderProcesses(OrderProcessSyncCollection orderProcessSyncCollection)
        {
            if (orderProcessSyncCollection != null && orderProcessSyncCollection.OrderProcesses != null &&
                orderProcessSyncCollection.OrderProcesses.Any() &&
                !string.IsNullOrEmpty(orderProcessSyncCollection.SerialNo))
            {

                await _userSyncService.AddUpdateSyncLog(orderProcessSyncCollection.BusinessId,
                    orderProcessSyncCollection.SerialNo, DateTime.UtcNow, TableNames.OrderProcesses,
                    ApiEndPoints.PostOrderProcesses, Guid.NewGuid().ToString(), orderProcessSyncCollection.Count);

                await _orderProcessService.AddUpdateOrderProcesses(orderProcessSyncCollection.OrderProcesses);
                return true;
            }

            return false;
        }
    }
}