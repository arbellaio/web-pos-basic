using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Transactions
{
    public class AccountTransactionSync : AuditEntity
    {
        [Key]
        public int AccountTransactionId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        
        // OrderCost + Tax if any and without Discount
        public decimal OrderAmount { get; set; }

        // OrderTotal is with Tax and Discount
        public decimal OrderCost { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public bool IsDeleted { get; set; }
        public string OrderToken { get; set; }
        public int OrderId { get; set; }
        public string Notes { get; set; }
        public string InvoiceNo { get; set; }
        public int OrderProcessId { get; set; }
        public int? AccountPaymentModeId { get; set; }

        public decimal ClosingAccountBalance { get; set; }

        public decimal OpeningAccountBalance { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }

    }
}
