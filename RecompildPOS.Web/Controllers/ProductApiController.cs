using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecompildPOS.Models.Sync;
using RecompildPOS.Services.Order;
using RecompildPOS.Services.Products;
using RecompildPOS.Services.Sync;
using RecompildPOS.Web.Helpers;
using RecompildPOS.Web.Models;

namespace RecompildPOS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserSyncService _userSyncService;

        public ProductApiController(IProductService productService, IUserSyncService userSyncService)
        {
            _productService = productService ?? throw new ArgumentNullException("productService");
            _userSyncService = userSyncService ?? throw new ArgumentNullException("userSyncService");
        }

        [HttpGet(ApiEndPoints.Products)]
        public async Task<ProductsSyncCollection> GetProducts(string serialNo, int businessId,
            DateTime requestedDateTime)
        {
            if (!string.IsNullOrEmpty(serialNo) && businessId > 0)
            {
                var syncLogInDb =
                    await _userSyncService.GetUserSyncLogBySerialNumberAndApiEndPoint(serialNo, ApiEndPoints.Products);


                var products = await _productService.GetProductsByBusinessId(businessId,
                    syncLogInDb?.LastUpdatedDate ?? requestedDateTime);

                var terminalLogId = Guid.NewGuid().ToString();

                await _userSyncService.AddUpdateSyncLog(businessId, serialNo, DateTime.UtcNow, TableNames.Products,
                    ApiEndPoints.Products, terminalLogId, products.Count());

                var productsSyncCollection = new ProductsSyncCollection()
                {
                    Products = products,
                    Count = products.Count(),
                    BusinessId = businessId,
                    SerialNo = serialNo,
                    TerminalLogId = terminalLogId
                };
                return productsSyncCollection;
            }

            return null;
        }

        [HttpPost(ApiEndPoints.PostProducts)]
        public async Task<bool> PostProducts(ProductsSyncCollection productsSyncCollection)
        {
            if (productsSyncCollection != null && productsSyncCollection.Products != null &&
                productsSyncCollection.Products.Any() && !string.IsNullOrEmpty(productsSyncCollection.SerialNo))
            {
                await _userSyncService.AddUpdateSyncLog(productsSyncCollection.BusinessId, productsSyncCollection.SerialNo, DateTime.UtcNow, TableNames.Products,ApiEndPoints.PostProducts, Guid.NewGuid().ToString(), productsSyncCollection.Count);
                await _productService.AddUpdateProducts(productsSyncCollection.Products);
                return true;
            }

            return false;
        }
    }
}