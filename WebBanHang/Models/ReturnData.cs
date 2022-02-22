using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class ReturnData
    {
        public int ResponseCode { get; set; }
        public string Description { get; set; }
        public string Extended { get; set; }
    }
}