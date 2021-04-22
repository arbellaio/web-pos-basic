using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Businesses;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BusinessApiController : ControllerBase
    {
        private readonly IUserSyncService _userSyncService;
        private readonly IBusinessService _businessService;
        
        public BusinessApiController(IBusinessService businessService, IUserSyncService userSyncService)
        {
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
            _businessService = businessService ?? throw new ArgumentNullException("businessService");
        }

        [HttpPost(ApiEndPoints.RegisterBusiness)]
        public async Task<RegisterBusinessSync> RegisterBusiness(RegisterBusinessSync registerBusiness)
        {
            if (registerBusiness != null && registerBusiness.Business != null && registerBusiness.User != null)
            {
                var registeredBusiness =
                    await _businessService.RegisterBusiness(registerBusiness.Business, registerBusiness.User);
                if (registeredBusiness != null)
                {
                    registeredBusiness.SerialNo = registerBusiness.SerialNo;
                    var terminalLogId = Guid.NewGuid().ToString();
                    await _userSyncService.AddUpdateSyncLog(registeredBusiness.Business.BusinessId, registeredBusiness.SerialNo, DateTime.UtcNow, TableNames.Business, ApiEndPoints.RegisterBusiness, terminalLogId,1);
                    return registeredBusiness;
                }
            }

            return null;
        }

        [HttpGet(ApiEndPoints.Business)]
        public async Task<BusinessSyncCollection> GetBusiness(string serialNo,int businessId, DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo))
            {
                var userSyncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo, ApiEndPoints.Business);
              
                    var business = await _businessService.GetBusinessByBusinessId(businessId,
                        userSyncLogInDb?.LastUpdatedDate ?? requestedDateTime);
                    if (business != null)
                    {
                        var terminalLogId = Guid.NewGuid().ToString();
                        var businessSyncCollection = new BusinessSyncCollection
                        {
                            Businesses = new List<BusinessSync>{business},
                            BusinessId = business.BusinessId,
                            SerialNo = serialNo,
                            Count = 1,
                            TerminalLogId = terminalLogId,
                            
                        };


                        await _userSyncService.AddUpdateSyncLog(business.BusinessId, serialNo, requestedDateTime,
                            TableNames.Business, ApiEndPoints.Business, terminalLogId, businessSyncCollection.Count);
                        return businessSyncCollection;
                    }
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostBusiness)]
        public async Task<bool> PostBusiness(BusinessSyncCollection businessSyncCollection)
        {
            if (businessSyncCollection != null && businessSyncCollection.Businesses != null &&
                businessSyncCollection.Businesses.Any())
            {
                foreach (var business in businessSyncCollection.Businesses)
                {
                    await _businessService.AddUpdateBusiness(business);

                    await _userSyncService.AddUpdateSyncLog(businessSyncCollection.BusinessId,
                        businessSyncCollection.SerialNo, DateTime.UtcNow, TableNames.Business,
                        ApiEndPoints.PostBusiness, businessSyncCollection.TerminalLogId, businessSyncCollection.Count);
                }

                return true;
            }

            return false;
        }

        [HttpGet(ApiEndPoints.LoginUrl)]
        public async Task<RegisterBusinessSync> Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {

                var loggedInUser = await _businessService.SyncBusinessUserOnLogin(username, password);

                return loggedInUser;
            }

            return null;
        }

    }
}