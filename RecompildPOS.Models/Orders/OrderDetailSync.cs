using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Orders
{
    public class OrderDetailSync : AuditEntity
    {
        [Key]
        public int OrderDetailId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public OrderSync Order { get; set; }
        public int OrderId { get; set; }
        public string Notes { get; set; }
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsDeleted { get; set; }
        public int? OrderDetailStatusId { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }

    }
}
