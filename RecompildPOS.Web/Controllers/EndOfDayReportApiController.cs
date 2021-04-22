using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.EndOfReports;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndOfDayReportApiController : ControllerBase
    {
        private readonly IEndOfDayReportService _endOfDayReportService;
        private readonly IUserSyncService _userSyncService;

        public EndOfDayReportApiController(IUserSyncService userSyncService,
            IEndOfDayReportService endOfDayReportService)
        {
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
            _endOfDayReportService = endOfDayReportService ?? throw new ArgumentNullException("endOfDayReportService");
        }

        [HttpGet(ApiEndPoints.EndOfDayReports)]
        public async Task<EndOfDayReportSyncCollection> GetEndOfDayReports(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo,
                        ApiEndPoints.EndOfDayReports);

                var endOfDayReports = await _endOfDayReportService.GetEndOfDayReportsByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, DateTime.UtcNow,
                    TableNames.EndOfDayReports, ApiEndPoints.EndOfDayReports, terminalLogId, endOfDayReports.Count());

                var endOfDayReportSyncCollection = new EndOfDayReportSyncCollection()
                {
                    EndOfDayReports = endOfDayReports,
                    Count = endOfDayReports.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                };
                return endOfDayReportSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostEndOfDayReports)]
        public async Task<bool> PostEndOfDayReports(EndOfDayReportSyncCollection endOfDayReportSyncCollection)
        {
            if (endOfDayReportSyncCollection != null && endOfDayReportSyncCollection.EndOfDayReports != null &&
                endOfDayReportSyncCollection.EndOfDayReports.Any() &&
                !string.IsNullOrEmpty(endOfDayReportSyncCollection.SerialNo))
            {
                
                await _userSyncService.AddUpdateSyncLog(endOfDayReportSyncCollection.BusinessId,
                    endOfDayReportSyncCollection.SerialNo, DateTime.UtcNow, TableNames.EndOfDayReports,
                    ApiEndPoints.PostEndOfDayReports, endOfDayReportSyncCollection.TerminalLogId,
                    endOfDayReportSyncCollection.Count);

                await _endOfDayReportService.AddUpdateEndOfDayReport(endOfDayReportSyncCollection.EndOfDayReports);
                return true;
            }

            return false;
        }
    }
}