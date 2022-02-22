using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using WebBanHang.Models.Metadata;
using tbl_ThanhVien = WebBanHang.Models.EF.tbl_ThanhVien;

namespace WebBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            tbl_SanPham sp = db.tbl_SanPham.SingleOrDefault(n => n.masp == iMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang spcheck = lstGioHang.SingleOrDefault(n => n.masp == iMaSP);
        
            if (spcheck != null)
            { 
                spcheck.soluong++;
                spcheck.ThanhTien = spcheck.soluong * spcheck.dongiasanpham;
                return PartialView("PartialGioHang");
            }
            GioHang itemGH = new GioHang(iMaSP);
            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView("PartialGioHang");
        }
        [HttpPost]
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(FormCollection ma, FormCollection sl)
        {
            string [] soluong = sl.GetValues("txtSoLuong");
            string[] masp = ma.GetValues("iMaSP");
            //Kiểm tra masp
           // tbl_SanPham sp = db.tbl_SanPham.SingleOrDefault(n => n.masp == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            //if (sp == null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            //GioHang sanpham = lstGioHang.SingleOrDefault(n => n.masp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
                for (int i = 0; i < lstGioHang.Count; i++)
                {
                    lstGioHang[i].masp = int.Parse(masp[i]);
                    lstGioHang[i].soluong = int.Parse(soluong[i]);
                    lstGioHang[i].ThanhTien = lstGioHang[i].soluong * lstGioHang[i].dongiasanpham;
                }
            // txtSoLuong = "0";
            return RedirectToAction("Index");
        }

        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMasp)
        {
            //Kiểm tra masp
            tbl_SanPham sp = db.tbl_SanPham.SingleOrDefault(n => n.masp == iMasp);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.masp == iMasp);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.masp == iMasp);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            if (Session["GioHang"] == null)
            { 
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.soluong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        #region // Mới hoàn thiện
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            tbl_Donhang ddh = new tbl_Donhang();
            tbl_KhachHang kh = (tbl_KhachHang)Session["use"];
            List<GioHang> gh = LayGioHang();
            ddh.makhachhang = kh.makhachhang;
            ddh.ngaydathang = DateTime.Now;
            Console.WriteLine(ddh);
            db.tbl_Donhang.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                tbl_ChiTietDonHang ctDH = new tbl_ChiTietDonHang();
                decimal thanhtien = item.soluong * (decimal)item.dongiasanpham;
                ctDH.madonhang = ddh.madonhang;
                ctDH.masp = item.masp;
                ctDH.soluong = item.soluong;
                //ctDH.dongi = (decimal)item.dDonGia;
                ctDH.tongtien = (double)thanhtien;
                db.tbl_ChiTietDonHang.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Donhangs");
        }
        #endregion
        public ActionResult GioHangTrong()
        {
            return View();
        }
        public ActionResult PartialGioHang()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }

        public ActionResult Thanhtoan()
        {

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //public JsonResult PaymentMethods(string paymentType)
        //{

        //}

    }
}