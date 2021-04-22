using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Products;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Products
{
    public interface IProductService
    {
        Task<List<ProductSync>> GetProductsByBusinessId(int businessId, DateTime lastModifiedDateTime);
        Task<ProductSync> GetProductById(int id);
        Task AddUpdateProducts(List<ProductSync> products);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDataContext _context;
        public ProductService(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public async Task<List<ProductSync>> GetProductsByBusinessId(int businessId, DateTime lastModifiedDateTime)
        {
            if (businessId > 0)
            {
                return await _context.Products.AsNoTracking().Where(x => x.BusinessId.Equals(businessId) && x.LastModifiedDate >= lastModifiedDateTime).ToListAsync();
            }

            return null;
        }

        public async Task<ProductSync> GetProductById(int id)
        {
            if (id > 0)
            {
                return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId.Equals(id));
            }

            return null;
        }

        public async Task AddUpdateProducts(List<ProductSync> products)
        {
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    product.LastModifiedDate = DateTime.UtcNow;

                    var productInDb = await GetProductById(product.ProductId);
                    if (productInDb == null)
                    {
                        await _context.Products.AddAsync(product);
                    }
                    else
                    {
                        _context.Products.Update(product);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
