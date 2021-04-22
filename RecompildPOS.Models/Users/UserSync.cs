using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Users
{
    public class UserSync : AuditEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }
        public int UserRole { get; set; }
        public string SerialNo { get; set; }
    }
}
