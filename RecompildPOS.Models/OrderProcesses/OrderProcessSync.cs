using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Accounts;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Orders;

namespace RecompildPOS.Models.OrderProcesses
{
    public class OrderProcessSync : AuditEntity
    {
        [Key]
        public int OrderProcessId { get; set; }
        public string OrderToken { get; set; }
        public int? OrderId { get; set; }
        public int? AccountId { get; set; }
        public string OrderNotes { get; set; }
        public decimal OrderProcessTotal { get; set; }
        public int? OrderProcessStatusId { get; set; }
        public int TransactionTypeId { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
        public List<OrderProcessDetailSync> OrderProcessDetails { get; set; }
    }
}
