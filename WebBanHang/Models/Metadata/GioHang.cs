using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.Models.Metadata
{
    public class GioHang
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        public int masp { get; set; }
        public string tensp { get; set; }
        public string anhsp1 { get; set; }
        public double dongiasanpham { get; set; }
        public int soluong { get; set; }
        public double ThanhTien
        {
            get;
            set;
        }
        public string tendanhmuc { get; set; }
        //Hàm tạo cho giỏ hàng
        public GioHang(int masp)
        {
            this.masp = masp;
            tbl_SanPham sp = db.tbl_SanPham.Single(n => n.masp == masp);
            this.tensp = sp.tensp;
            this.anhsp1 = sp.anhsp1;
            this.tendanhmuc = db.tbl_DanhMucSanPham.Where(x => x.madanhmuc == sp.madanhmuc).Select(x => x.tendanhmuc).SingleOrDefault();
            this.dongiasanpham = double.Parse(sp.giasanpham.ToString());
            soluong = 1;
            this.ThanhTien = (double)(soluong * sp.giasanpham);
        }
    }
}