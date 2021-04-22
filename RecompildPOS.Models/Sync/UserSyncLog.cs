using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RecompildPOS.Models.Audit;
using RecompildPOS.Models.Businesses;

namespace RecompildPOS.Models.Sync
{
    public class UserSyncLog
    {
        [Key]
        public int UserSyncId { get; set; }
        public string SerialNo { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string TableName { get; set; }
        public string ApiEndPoint { get; set; }
        public int Count { get; set; }
        public int AckCount { get; set; }
        public string TerminalLogId { get; set; }

        [ForeignKey(nameof(BusinessId))]
        public BusinessSync Business { get; set; }
        public int BusinessId { get; set; }

    }
}
