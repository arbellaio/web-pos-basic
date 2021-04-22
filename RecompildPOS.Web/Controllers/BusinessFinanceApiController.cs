using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Finances;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessFinanceApiController : ControllerBase
    {
        private readonly IBusinessFinanceService _businessFinanceService;
        private readonly IUserSyncService _userSyncService;

        public BusinessFinanceApiController(IUserSyncService userSyncService,
            IBusinessFinanceService businessFinanceService)
        {
            _userSyncService = userSyncService ?? throw new ArgumentNullException();
            _businessFinanceService = businessFinanceService ?? throw new ArgumentNullException();
        }

        [HttpGet(ApiEndPoints.BusinessFinances)]
        public async Task<BusinessFinanceSyncCollection> GetAllBusinessFinances(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo,
                        ApiEndPoints.BusinessFinances);

                var businessFinances =
                    await _businessFinanceService.GetBusinessFinancesByBusinessId(businessId,
                        syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime,
                    TableNames.BusinessFinances, ApiEndPoints.BusinessFinances, terminalLogId,
                    businessFinances.Count());

                var businessFinanceCollection = new BusinessFinanceSyncCollection
                {
                    BusinessFinances = businessFinances,
                    Count = businessFinances.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                    TerminalLogId = terminalLogId
                };
                return businessFinanceCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostBusinessFinances)]
        public async Task<bool> PostBusinessFinances(BusinessFinanceSyncCollection businessFinanceSyncCollection)
        {
            if (businessFinanceSyncCollection != null && businessFinanceSyncCollection.BusinessFinances != null &&
                businessFinanceSyncCollection.BusinessFinances.Any())
            {
                await _userSyncService.AddUpdateSyncLog(businessFinanceSyncCollection.BusinessId,
                    businessFinanceSyncCollection.SerialNo, DateTime.UtcNow, TableNames.BusinessFinances,
                    ApiEndPoints.PostBusinessFinances, businessFinanceSyncCollection.TerminalLogId,
                    businessFinanceSyncCollection.Count);

                await _businessFinanceService.AddUpdateBusinessFinances(businessFinanceSyncCollection.BusinessFinances);
                return true;
            }

            return false;
        }
    }
}