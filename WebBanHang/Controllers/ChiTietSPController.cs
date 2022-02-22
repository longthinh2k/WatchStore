using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Common;
using WebBanHang.Models;
using WebBanHang.Models.DAO;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class ChiTietSPController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: ChiTietSP
        public ActionResult Index(long id)
        {
            var dao = new SANPHAMDAO();
            var binhluan = new tbl_BinhLuan();
            var thanhvien = new tbl_ThanhVien();
            var model = dao.infoSP(id);

           
            var dao2 = db.tbl_BinhLuan.Where(n => n.masp == id).ToList();

            
            ViewBag.ThanhVien = db.tbl_KhachHang.ToList();
            ViewBag.BinhLuan = dao2;
            var tv = db.tbl_KhachHang.Find(dao2.First().makhachhang);
            ViewBag.tenkhachhang = tv.tenkhachhang;
            return View(model);
        }
        // [HttpPost]
        public JsonResult PostBinhLuan(int id, string binhluan)
        {
            var returnData = new ReturnData();
            // KHI NHẬN ĐƯỢC SCACS THÔNG TIN TỪ WIEW TRUYỀN XUỐNG THÌ CHECK XEM TÀI KHOẢN ĐÃ ĐĂNG NHẬP

            var taikhoan = Session["TaiKhoan"] != null ? Session["TaiKhoan"].ToString() : string.Empty;
            UserLogin session = (UserLogin)Session["USER_SESSION"];
            int makh = Convert.ToInt32(session.taikhoan.MaThanhVien);
            if (taikhoan == null || string.IsNullOrEmpty(taikhoan))
            {
                returnData.ResponseCode = -11;
                returnData.Description = "Vui lòng đăng nhập!";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            var ma_khach_hang = Session["MaKhacHang"] != null ? Session["MaKhacHang"].ToString() : string.Empty;
            if (ma_khach_hang == null || string.IsNullOrEmpty(ma_khach_hang))
            {
                returnData.ResponseCode = -121;
                returnData.Description = "Vui lòng đăng nhập!";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tbl_BinhLuan bl = new tbl_BinhLuan();
                bl.noidung = binhluan;
                bl.ngaybinhluan = DateTime.Now;
                bl.masp = id;
                var makhachang = db.tbl_KhachHang.Where(x => x.MaThanhVien == makh).ToList();
                bl.makhachhang = makhachang.First().makhachhang;
                db.tbl_BinhLuan.Add(bl);
                db.SaveChanges();
                returnData.ResponseCode = 1;
                returnData.Description = "Gửi bình luận thành công!!";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

    }
}