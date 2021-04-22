using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Orders
{
    public class OrderSync : AuditEntity
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string Notes { get; set; }
        public int? AccountId { get; set; }
        public int? StatusId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? OrderCost { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
        public List<OrderDetailSync> OrderDetails { get; set; }
    }
}
