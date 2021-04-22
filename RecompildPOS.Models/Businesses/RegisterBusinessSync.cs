using System;
using System.Collections.Generic;
using System.Text;
using RecompildPOS.Models.Users;

namespace RecompildPOS.Models.Businesses
{
    public class RegisterBusinessSync
    {
        public BusinessSync Business { get; set; }
        public UserSync User { get; set; }
        public string SerialNo { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
