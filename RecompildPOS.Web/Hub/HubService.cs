using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.Businesses;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;

namespace RecompildPOS.Services.Hub
{

    public class HubService
    {
        private static  IUserSyncService _userSyncService;
        private static  IBusinessService _businessService;

        public HubService(IBusinessService businessService, IUserSyncService userSyncService)
        {
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
            _businessService = businessService ?? throw new ArgumentNullException("businessService");
        }

        public static async Task<RegisterBusinessSync> RegisterBusiness(RegisterBusinessSync registerBusiness)
        {
            Console.WriteLine("Inside HubService");
            if (registerBusiness != null && registerBusiness.Business != null && registerBusiness.User != null)
            {
                var registeredBusiness =
                    await _businessService.RegisterBusiness(registerBusiness.Business, registerBusiness.User);
                if (registeredBusiness != null)
                {
                    registeredBusiness.SerialNo = registerBusiness.SerialNo;
                    var terminalLogId = Guid.NewGuid().ToString();
                    await _userSyncService.AddUpdateSyncLog(registeredBusiness.Business.BusinessId, registeredBusiness.SerialNo, DateTime.UtcNow, TableNames.Business, ApiEndPoints.RegisterBusiness, terminalLogId, 1);
                    Console.WriteLine("Success HubService");
                    return registeredBusiness;
                }
            }

            return null;
        }
    }
}