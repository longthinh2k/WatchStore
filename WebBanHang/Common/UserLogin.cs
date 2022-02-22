using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.Common
{
    public class UserLogin
    {
        public tbl_ThanhVien taikhoan { get; set; }
        public long? makhachhang { get; set; }
    }
}