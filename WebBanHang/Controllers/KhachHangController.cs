using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Common;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{

    public class KhachHangController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: KhachHang
        [HttpGet]
        public ActionResult TaiKhoan()
        {
            var session = Session["USER_SESSION"]as UserLogin;
            var data = db.tbl_ThanhVien.Find(session.makhachhang);
            var khachhang = db.tbl_KhachHang.Where(x => x.MaThanhVien == data.MaThanhVien).ToList();


            mathanhvien = khachhang.First().makhachhang;
            return View(data);
        }
        
        public static long mathanhvien;
       
      [HttpPost]
     public ActionResult Edit(tbl_ThanhVien thanhvien)
        {
            var data = db.tbl_ThanhVien.Find(mathanhvien);
            data.HoTen = thanhvien.HoTen;
            data.MatKhau = thanhvien.MatKhau;
            data.SoDienThoai = thanhvien.SoDienThoai;
            data.DiaChi = thanhvien.DiaChi;
            data.TaiKhoan = thanhvien.TaiKhoan;
            data.Email = thanhvien.Email;
            db.SaveChanges();
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult DonHang()
        {
            var data = db.tbl_Donhang.Where(x => x.makhachhang == mathanhvien && x.IsDaxoa == 0).ToList();

            return View(data);
        }
        public ActionResult DonHangDaHuy()
        {
            var data = db.tbl_Donhang.Where(x => x.makhachhang == mathanhvien && x.IsDaxoa == 1).ToList();

            return View(data);
        }
    }

}