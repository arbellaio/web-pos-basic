using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Accounts;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IUserSyncService _userSyncService;
        private readonly IAccountsService _accountsService;

        public AccountApiController(IAccountsService accountsService, IUserSyncService userSyncService)
        {
            _accountsService = accountsService ?? throw new ArgumentNullException("accountsService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.Accounts)]
        public async Task<AccountSyncCollection> GetAccounts(string serialNo, int businessId, DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo, ApiEndPoints.Accounts);
               

                var accounts = await _accountsService.GetAllAccountsByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime,
                    TableNames.Accounts, ApiEndPoints.Accounts, terminalLogId,
                    accounts.Count());

                var accountSyncCollection = new AccountSyncCollection
                {
                    Accounts = accounts,
                    Count = accounts.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                };
                return accountSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostAccounts)]
        public async Task<bool> PostAccounts(AccountSyncCollection accountSyncCollections)
        {
            if (accountSyncCollections != null && accountSyncCollections.Accounts != null &&
                accountSyncCollections.Accounts.Any() && !string.IsNullOrEmpty(accountSyncCollections.SerialNo))
            {
                await _userSyncService.AddUpdateSyncLog(accountSyncCollections.BusinessId,
                    accountSyncCollections.SerialNo, DateTime.UtcNow, TableNames.Accounts, ApiEndPoints.PostAccounts,
                    accountSyncCollections.TerminalLogId, accountSyncCollections.Count);

                await _accountsService.AddUpdateAccounts(accountSyncCollections.Accounts);

                return true;
            }

            return false;
        }
    }
}