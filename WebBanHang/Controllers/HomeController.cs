using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanHang.Common;
using WebBanHang.Models.DAO;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        HOMEDAO dao = new HOMEDAO();
        QLBanHangDBContext db = new QLBanHangDBContext();

        public ActionResult Index()
        {      
            var model = dao.getViewListSP();
            ViewBag.DanhMucMoi = dao.GetNewSP();
            ViewBag.DanhMucGia = dao.GetGiaSP();
            return View(model);
        }



        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string TaiKhoan = f["TenDangNhap"].ToString();
            string MatKhau = f["MatKhau"].ToString();
            var tv = dao.info(TaiKhoan, MatKhau);
            if (tv != null)
            {
                UserLogin user = new UserLogin();
                Session["TaiKhoan"] = tv;
                Session["MaKhacHang"] = tv.MaThanhVien;
                user.taikhoan = tv;
                user.makhachhang = tv.MaThanhVien;
                Session.Add(TrangThaiDangNhap.USER_SESSION, user);
               // return RedirectToAction("Index");
                return Content("<script>window.location.reload()</script>");
            }
           else return Content("Tên đăng nhập hoặc mật khẩu không đúng");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            tbl_ThanhVien tv = new tbl_ThanhVien();
            return View(tv);
        }
        [HttpPost]
        public ActionResult DangKy(tbl_ThanhVien thanhvien)
        {
            if(ModelState.IsValid)
            {
                ViewBag.ThongBao = "Thêm thành công";
                //var tv = dao.DangKyPost(thanhvien);
                //return View(tv);
                db.tbl_ThanhVien.Add(thanhvien);
                db.SaveChanges();
                tbl_KhachHang kh = new tbl_KhachHang();
                kh.diachi = thanhvien.DiaChi;
                kh.tenkhachhang = thanhvien.HoTen;
                kh.sdt = Convert.ToInt32(thanhvien.SoDienThoai);
                kh.gmail = thanhvien.Email;
                kh.MaThanhVien = thanhvien.MaThanhVien;
                db.tbl_KhachHang.Add(kh);
                db.SaveChanges();
            }    
            else
            {
                ViewBag.ThongBao = "Thêm thất bại";
            }
            return View(thanhvien);
        }

        [ChildActionOnly]

        public ActionResult MenuPartial()
        {
            var lstmenu = db.tbl_DanhMucSanPham.ToList();
            return PartialView(lstmenu);
        }
    }



}