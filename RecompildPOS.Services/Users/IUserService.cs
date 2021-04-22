using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Users
{
    public interface IUserService
    {
        Task<List<UserSync>> GetUsersByBusinessId(int businessId, DateTime lastSyncDate);
        Task<UserSync> GetUserById(int id);
        Task<UserSync> GetUserBySerial(string serial);
        Task AddUpdateUsers(List<UserSync> users);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDataContext _context;

        public UserService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public async Task<List<UserSync>> GetUsersByBusinessId(int businessId, DateTime lastSyncDate)
        {
            if (businessId > 0)
            {
                return await _context.Users.AsNoTracking()
                    .Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastSyncDate).ToListAsync();
            }

            return null;
        }

        public async Task<UserSync> GetUserById(int id)
        {
            if (id > 0)
            {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(id));
            }

            return null;
        }

        public async Task<UserSync> GetUserBySerial(string serial)
        {
            if (!string.IsNullOrEmpty(serial))
            {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.SerialNo.Equals(serial));
            }

            return null;
        }

        public async Task AddUpdateUsers(List<UserSync> users)
        {
            if (users != null && users.Any())
            {
                foreach (var user in users)
                {
                    user.LastModifiedDate = DateTime.UtcNow;
                    var userInDb = await GetUserById(user.UserId);
                    if (userInDb == null)
                    {
                        await _context.Users.AddAsync(user);
                    }
                    else
                    {
                        _context.Users.Update(user);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}