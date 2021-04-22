using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Expense;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessExpenseApiController : ControllerBase
    {
        private readonly IBusinessExpenseService _businessExpenseService;
        private readonly IUserSyncService _userSyncService;

        public BusinessExpenseApiController(IBusinessExpenseService businessExpenseService,
            IUserSyncService userSyncService)
        {
            _businessExpenseService =
                businessExpenseService ?? throw new ArgumentNullException("businessExpenseService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.BusinessExpenses)]
        public async Task<BusinessExpenseSyncCollection> GetBusinessExpenses(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo,
                        ApiEndPoints.BusinessExpenses);


                var businessExpenses = await _businessExpenseService.GetBusinessExpensesByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime,
                    TableNames.BusinessExpenses, ApiEndPoints.BusinessExpenses, terminalLogId,
                    businessExpenses.Count());

                var businessExpenseSyncCollection = new BusinessExpenseSyncCollection()
                {
                    BusinessExpenses = businessExpenses,
                    Count = businessExpenses.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                };
                return businessExpenseSyncCollection;
            }

            return null;
        }


        [HttpPost(ApiEndPoints.PostBusinessExpenses)]
        public async Task<bool> PostBusinessExpenses(BusinessExpenseSyncCollection businessExpenseSyncCollection)
        {
            if (businessExpenseSyncCollection != null && businessExpenseSyncCollection.BusinessExpenses != null &&
                businessExpenseSyncCollection.BusinessExpenses.Any() &&
                !string.IsNullOrEmpty(businessExpenseSyncCollection.SerialNo))
            {
                await _userSyncService.AddUpdateSyncLog(businessExpenseSyncCollection.BusinessId,
                    businessExpenseSyncCollection.SerialNo, DateTime.UtcNow, TableNames.BusinessExpenses,
                    ApiEndPoints.PostBusinessExpenses, businessExpenseSyncCollection.TerminalLogId,
                    businessExpenseSyncCollection.Count);

                await _businessExpenseService.AddUpdateBusinessExpenses(businessExpenseSyncCollection.BusinessExpenses);
                return true;
            }

            return false;
        }
    }
}