using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.Models.ViewModel
{
    public class ViewDonHang
    {
        public long? idDonhang { get; set; }
        public string tenkhachhang{ get; set; }
        public DateTime? thoigiandathang { get; set; }
        public int? sdt { get; set; }
        public string hinhthucthanhtoan { get; set; }
        public string diachi { get; set; } 
        //public string anhsp { get; set; }

        //public long? IDSanPham { get; set; }
        //public string tensp { get; set; }
        public List<int?> soluong { get; set; }
        //public long? IDDanhMuc { get; set; }
        //public string danhmuc { get; set; }
        
        public List<string > danhmuc { get; set; }
        public List<tbl_SanPham> sanPhams { get; set; }
        public string magiamgia { get; set; }
        public double? tongtien { get; set; }

    }
}