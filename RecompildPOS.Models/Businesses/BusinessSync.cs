using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RecompildPOS.Models.Audit;

namespace RecompildPOS.Models.Businesses
{
    public class BusinessSync : AuditEntity
    {
        [Key]
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string OwnerName { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsDeleted { get; set; }

        //Business Owner
        public int BusinessOwnerUserId { get; set; }
    }
}
