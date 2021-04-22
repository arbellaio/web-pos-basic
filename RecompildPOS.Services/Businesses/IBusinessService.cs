using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.DataContext;
using RecompildPOS.Services.Helpers;

namespace RecompildPOS.Services.Businesses
{
    public interface IBusinessService
    {
        Task AddUpdateBusiness(BusinessSync businesses);
        Task<BusinessSync> GetBusinessByBusinessId(int id, DateTime lastModifiedDateTime);
        Task<BusinessSync> GetBusinessByBusinessId(int id);
        Task<BusinessSync> GetBusinessByBusinessOwnerUserId(int userId);
        Task<RegisterBusinessSync> RegisterBusiness(BusinessSync business, UserSync user);
        Task<RegisterBusinessSync> SyncBusinessUserOnLogin(string username, string password);
    }

    public class BusinessService : IBusinessService
    {
        private readonly ApplicationDataContext _context;

        public BusinessService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("");
        }

        public async Task AddUpdateBusiness(BusinessSync business)
        {
            var businessInDb = await GetBusinessByBusinessId(business.BusinessId);
            if (businessInDb != null)
            {
                _context.Businesses.Update(business);
            }
            else
            {
                await _context.Businesses.AddAsync(business);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<BusinessSync> GetBusinessByBusinessId(int id, DateTime lastModifiedDateTime)
        {
            if (id > 0)
            {
                return await _context.Businesses.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.BusinessId.Equals(id) && x.LastModifiedDate > lastModifiedDateTime);
            }

            return null;
        }


        public async Task<BusinessSync> GetBusinessByBusinessId(int id)
        {
            if (id > 0)
            {
                return await _context.Businesses.AsNoTracking().FirstOrDefaultAsync(x => x.BusinessId.Equals(id));
            }

            return null;
        }

        public async Task<BusinessSync> GetBusinessByBusinessOwnerUserId(int userId)
        {
            if (userId > 0)
            {
                return await _context.Businesses.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BusinessOwnerUserId.Equals(userId));
            }

            return null;
        }

        public async Task<RegisterBusinessSync> RegisterBusiness(BusinessSync business, UserSync user)
        {
            if (user != null && business != null)
            {
                var businessInDb = await GetBusinessByBusinessId(business.BusinessId);
                var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId.Equals(user.UserId));
                if (businessInDb != null)
                {
                    _context.Businesses.Update(business);
                }
                else
                {
                    await _context.Businesses.AddAsync(business);
                }

                await _context.SaveChangesAsync();

                user.BusinessId = business.BusinessId;
                if (userInDb == null)
                {
                    var hashedPassword = PasswordHasher.HashPassword(user.Password);
                    user.Password = hashedPassword;
                    await _context.Users.AddAsync(user);
                }
                else
                {
                    _context.Users.Update(user);
                }

                return new RegisterBusinessSync
                {
                    Business = business,
                    User = user
                };
            }

            return null;
        }

        public async Task<RegisterBusinessSync> SyncBusinessUserOnLogin(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.Username.Equals(username));
           

            if (user != null)
            {
                var isPasswordVerified = PasswordHasher.VerifyHashedPassword(user.Password, password);
                if (!isPasswordVerified)
                    return null;

                var businessInDb =
                    await _context.Businesses.FirstOrDefaultAsync(x => x.BusinessId.Equals(user.BusinessId));
                if (businessInDb != null)
                {
                    var registerBusinessSync = new RegisterBusinessSync
                    {
                        Business = businessInDb,
                        User = user
                    };
                    return registerBusinessSync;
                }
            }

            return null;
        }
    }
}