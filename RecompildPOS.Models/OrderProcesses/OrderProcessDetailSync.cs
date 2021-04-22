using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.OrderProcesses
{
    public class OrderProcessDetailSync : AuditEntity
    {
        [Key]
        public int OrderProcessDetailId { get; set; }

        [ForeignKey(nameof(OrderProcessId))] 
        public OrderProcessSync OrderProcess { get; set; }
        public int OrderProcessId { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityProcessed { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
