using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Products
{
    public class ProductSync : AuditEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public string BarCode1 { get; set; }
        public string BarCode2 { get; set; }
        public string QrCode { get; set; }
        public decimal SellPrice { get; set; }
        public decimal? BuyPrice { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public decimal Tax { get; set; }
        public string ProductNotes { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }



    }
}
