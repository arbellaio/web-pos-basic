using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RecompildPOS.Models.Audit;

namespace RecompildPOS.Models.Sync
{
    public class SyncLog : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public string Request { get; set; }
        public string TableName { get; set; }
        public string SerialNo { get; set; }
        public int ErrorCode { get; set; }
        public bool Synced { get; set; }
        public int ResultCount { get; set; }
        public bool IsPost { get; set; }
        public bool IsPending { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime LastSynced { get; set; }


    }
}
