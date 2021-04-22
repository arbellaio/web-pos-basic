using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Sync;
using RecompildPOS.Services.Users;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserSyncService _userSyncService;

        public UsersApiController(IUserService userService, IUserSyncService userSyncService)
        {
            _userService = userService ?? throw new ArgumentNullException("userService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.Users)]
        public async Task<UserSyncCollection> GetUsers(string serialNo,int businessId ,DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var userSyncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo, ApiEndPoints.Users);

                var users = await _userService.GetUsersByBusinessId(businessId,
                    userSyncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();


                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, requestedDateTime, TableNames.Users,
                    ApiEndPoints.Users, terminalLogId, users.Count());

                if (users != null && users.Any())
                {
                    var userSyncCollection = new UserSyncCollection
                    {
                        Users = users,
                        Count = users.Count(),
                        TerminalLogId = terminalLogId,
                    };
                    return userSyncCollection;
                }
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostUsers)]
        public async Task<bool> PostUsers(UserSyncCollection userSyncCollection)
        {
            if (userSyncCollection != null && userSyncCollection.Users != null && userSyncCollection.Count > 0 &&
                userSyncCollection.Users.Any())
            {
                await _userSyncService.AddUpdateSyncLog(userSyncCollection.BusinessId, userSyncCollection.SerialNo,
                    DateTime.UtcNow, TableNames.Users, ApiEndPoints.PostUsers, userSyncCollection.TerminalLogId,
                    userSyncCollection.Count);
                await _userService.AddUpdateUsers(userSyncCollection.Users);
                return true;
            }

            return false;
        }
    }
}