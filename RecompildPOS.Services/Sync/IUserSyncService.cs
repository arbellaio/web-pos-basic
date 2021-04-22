using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Sync;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Sync
{
    public interface IUserSyncService
    {
        Task<UserSyncLog> GetUserSyncLogBySerialNumber(string number);

        Task AddUpdateSyncLog(int businessId, string serialNo, DateTime lastUpdateDateTime, string tableName,
            string apiEndPoint, string terminalLogId, int count);

        Task<UserSyncLog> GetUserSyncLogBySerialNumberAndApiEndPoint(string number, string apiEndPoint);
        Task<UserSyncLog> GetUserSyncLogBySerialNumberAndTableName(string number, string table);
        Task<UserSync> GetUserBySerial(string serialNo);
        Task<bool> VerifyAcknowledgment(string terminalLogId, int count, string serialNo);
    }

    public class UserSyncService : IUserSyncService
    {
        private readonly ApplicationDataContext _context;

        public UserSyncService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public async Task<UserSyncLog> GetUserSyncLogBySerialNumber(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                return await _context.UserSyncLogs.FirstOrDefaultAsync(x => x.SerialNo.Equals(number));
            }

            return null;
        }


        public async Task<UserSyncLog> GetUserSyncLogBySerialNumberAndApiEndPoint(string number, string apiEndPoint)
        {
            if (!string.IsNullOrEmpty(number))
            {
                var userSyncLog =  await _context.UserSyncLogs.LastOrDefaultAsync(x => x.SerialNo.Equals(number) && x.ApiEndPoint.Equals(apiEndPoint));
                if(userSyncLog == null){
                    var business = await _context.Users.FirstOrDefaultAsync(x => x.SerialNo.Equals(number));
                    userSyncLog = new UserSyncLog
                    {
                            BusinessId = business.BusinessId,
                            LastUpdatedDate = DateTime.UtcNow,
                            SerialNo = number,
                            ApiEndPoint = apiEndPoint,
              
                    };
                }
                return userSyncLog;
            }

            return null;
        }

        public async Task<UserSyncLog> GetUserSyncLogBySerialNumberAndTableName(string number, string table)
        {
            if (!string.IsNullOrEmpty(number))
            {
                return await _context.UserSyncLogs.FirstOrDefaultAsync(x => x.SerialNo.Equals(number));
            }

            return null;
        }

        public async Task AddUpdateSyncLog(int businessId, string serialNo, DateTime lastUpdateDateTime,
            string tableName, string apiEndPoint, string terminalLogId, int count)
        {
            var userSyncLog = new UserSyncLog
            {
                BusinessId = businessId,
                LastUpdatedDate = lastUpdateDateTime,
                SerialNo = serialNo,
                ApiEndPoint = apiEndPoint,
                TableName = tableName,
                TerminalLogId = terminalLogId,
                Count = count,
            };
            await _context.UserSyncLogs.AddAsync(userSyncLog);
            await _context.SaveChangesAsync();
        }


        public async Task<UserSync> GetUserBySerial(string serialNo)
        {
            if (!string.IsNullOrEmpty(serialNo))
            {
                var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.SerialNo.Equals(serialNo));
                return userInDb;
            }

            return null;
        }

        public async Task<bool> VerifyAcknowledgment(string terminalLogId, int count, string serialNo)
        {
            var userSync = await _context.UserSyncLogs.FirstOrDefaultAsync(x =>
                x.TerminalLogId.Equals(terminalLogId) && x.SerialNo.Equals(serialNo));
            if (userSync != null)
            {
                if (userSync.Count == count)
                {
                    userSync.AckCount = count;
                    _context.UserSyncLogs.Update(userSync);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}