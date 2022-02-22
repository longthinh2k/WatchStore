using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Areas.Admin.ViewModel
{
    public class ViewChiTietBinhLuan
    {
        public long? idBinhLuan { get; set; }
        public long? idSanPham { get; set; }
        public long? idKhachHang { get; set; }
        public string tenKhachHang { get; set; }
        public string noiDung { get; set; }
        public DateTime? ngayBinhLuan { get; set; }
    }
}