using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcknowledgementApiController : ControllerBase
    {
        private readonly IUserSyncService _userSyncService;
        public AcknowledgementApiController(IUserSyncService userSyncService)
        {
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }
        [HttpGet(ApiEndPoints.VerifyApi)]
        public async Task<HttpResponseMessage> VerifyResponse(string terminalLogId, int count, string serialNo)
        {
            if (!string.IsNullOrEmpty(serialNo) && count > 0 && !string.IsNullOrEmpty(terminalLogId))
            {
                var acknowledgment = await _userSyncService.VerifyAcknowledgment(terminalLogId, count, serialNo);
                if (acknowledgment)
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            } 
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        [HttpGet(ApiEndPoints.ServerPing)]
        public async Task<HttpResponseMessage> ServerPing(string serialNo)
        {
            if (!string.IsNullOrEmpty(serialNo))
            {
                var userSync = await _userSyncService.GetUserSyncLogBySerialNumber(serialNo);
                if (userSync != null)
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            } 
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}