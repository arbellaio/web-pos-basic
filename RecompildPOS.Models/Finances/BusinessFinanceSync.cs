using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Finances
{
    public class BusinessFinanceSync : AuditEntity
    {
        [Key]
        public int BusinessFinanceId { get; set; }
        public decimal MonthlyEarning { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
