using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.InventoryStocks
{
    public class InventoryStockSync : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal InStock { get; set; }
        public decimal Available { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
    }
}
