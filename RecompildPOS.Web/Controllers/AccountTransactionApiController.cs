using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Models.Transactions;
using RecompildPOS.Services.Order;
using RecompildPOS.Services.Sync;
using RecompildPOS.Services.Transactions;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionApiController : ControllerBase
    {
        private readonly IAccountTransactionService _accountTransactionService;
        private readonly IUserSyncService _userSyncService;

        public AccountTransactionApiController(IAccountTransactionService accountTransactionService,
            IUserSyncService userSyncService)
        {
            _accountTransactionService = accountTransactionService ??
                                         throw new ArgumentNullException("accountTransactionService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.AccountTransactions)]
        public async Task<AccountTransactionSyncCollection> GetAccountTransactions(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo,
                        ApiEndPoints.AccountTransactions);

                var accountTransactions = await _accountTransactionService.GetAccountTransactionByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime,
                    TableNames.AccountTransactions, ApiEndPoints.AccountTransactions, terminalLogId,
                    accountTransactions.Count());

                var accountTransactionSyncCollection = new AccountTransactionSyncCollection()
                {
                    AccountTransactions = accountTransactions,
                    Count = accountTransactions.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                };
                return accountTransactionSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostAccountTransactions)]
        public async Task<bool> PostAccountTransactions(AccountTransactionSyncCollection accountTransactionSyncCollection)
        {
            if (accountTransactionSyncCollection != null &&
                accountTransactionSyncCollection.AccountTransactions != null &&
                accountTransactionSyncCollection.AccountTransactions.Any() &&
                !string.IsNullOrEmpty(accountTransactionSyncCollection.SerialNo))
            {
                await _userSyncService.AddUpdateSyncLog(accountTransactionSyncCollection.BusinessId,
                    accountTransactionSyncCollection.SerialNo, DateTime.UtcNow, TableNames.AccountTransactions,
                    ApiEndPoints.PostAccountTransactions, accountTransactionSyncCollection.TerminalLogId,
                    accountTransactionSyncCollection.Count);

                await _accountTransactionService.AddUpdateAccountTransactions(accountTransactionSyncCollection
                    .AccountTransactions);
                return true;
            }

            return false;
        }
    }
}