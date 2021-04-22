using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Accounts
{
    public class AccountSync : AuditEntity
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountCode { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal CreditLimit { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }

    }
}
