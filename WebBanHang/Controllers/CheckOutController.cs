using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang.Common;
using WebBanHang.Models;
using WebBanHang.Models.DAO;
using WebBanHang.Models.EF;
using WebBanHang.Models.Metadata;

namespace WebBanHang.Controllers
{
    public class CheckOutController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: CheckOut
        public ActionResult Index()
        {
           

            return View();
        }

        public JsonResult PaymentByCOD(string fullname, string address, int mobile, string voucher)
        {
            
            
            var returnData = new ReturnData();
            try
            {
                // KHI NHẬN ĐƯỢC SCACS THÔNG TIN TỪ WIEW TRUYỀN XUỐNG THÌ CHECK XEM TÀI KHOẢN ĐÃ ĐĂNG NHẬP

                var taikhoan= Session["TaiKhoan"]!=null ? Session["TaiKhoan"].ToString():string.Empty;
                if(taikhoan==null || string.IsNullOrEmpty(taikhoan))
                {
                    returnData.ResponseCode = -11;
                    returnData.Description = "Vui lòng đăng nhập!"; 
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var ma_khach_hang = Session["MaKhacHang"] != null ? Session["MaKhacHang"].ToString() : string.Empty;
                UserLogin session = (UserLogin)Session["USER_SESSION"];
                int makh = Convert.ToInt32(session.taikhoan.MaThanhVien);
                if (ma_khach_hang == null || string.IsNullOrEmpty(ma_khach_hang))
                {
                    returnData.ResponseCode = -121;
                    returnData.Description = "Vui lòng đăng nhập!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


                // bƯỚC 2 :  lấy thông tin trong giỏ hàng 
                var list = LayGioHang();
                if (list == null || list.Count <= 0)
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Thông tin đơn hàng không hợp lệ!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }

                // Bước 3: Gọi vào database để lưu thông tin đơn hàng 

                var donhang = new tbl_Donhang();
               //var khachhang = new tbl_KhachHang();
                //khachhang.makhachhang = 1;
                //khachhang.tenkhachhang = fullname;
                //donhang.tbl_KhachHang = khachhang;

                double totalAmount = 0;

                if (!string.IsNullOrEmpty(voucher))
                {
                    if (voucher == "GIAM5")
                    {
                        var voucherValue = list.Sum(s => s.ThanhTien) * 5 / 100;

                        totalAmount = list.Sum(s => s.ThanhTien) - voucherValue;
                    }

                }
                else
                {
                    totalAmount = list.Sum(s => s.ThanhTien);

                }
                var makhachang = db.tbl_KhachHang.Where(x => x.MaThanhVien == makh).ToList();
                donhang.makhachhang = makhachang.First().makhachhang;
                donhang.tenkhachhang = fullname;
                donhang.ngaydathang = DateTime.Now;
                donhang.trangthai = "COD";// cá
                donhang.tongtien = totalAmount;
                donhang.diachi = address;
                donhang.sdt = mobile;
                donhang.magiamgia = voucher;
                donhang.trangthaidathang = "Tạo mới";
                donhang.IsDaxoa = 0;



                // insert vào db 
                db.tbl_Donhang.Add(donhang);
                var result = db.SaveChanges();
                madonhang = donhang.madonhang;

                if (result > 0)
                {
                    // insert xong đơn hàng thì insert vào chi tiết đơn hàng 
                    var ma_don_hang = donhang.madonhang;
                    foreach (var item in list) // list có nhiều sản phẩm thì insert nhiều sản phẩm
                    {
                        var chitietdon = new tbl_ChiTietDonHang();

                        chitietdon.madonhang = ma_don_hang;
                        chitietdon.masp = item.masp;
                        chitietdon.soluong = item.soluong;
                        chitietdon.tongtien = item.ThanhTien;

                        db.tbl_ChiTietDonHang.Add(chitietdon);

                        var result_chitiet = db.SaveChanges();
                    }

                    //SET LẠI GIỎ HÀNG BẰNG RỖNG 
                    Session["GioHang"] = null;
                    var bas64str = Base64Encode(ma_don_hang + "|" + totalAmount + "|" + mobile);
                    returnData.Extended = bas64str;
                    returnData.ResponseCode = 1;
                    returnData.Description = "Đăt hàng thành công!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Đăt hàng thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

               return Json(returnData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static long madonhang;
        public JsonResult PaymentByBank(string fullname, string address, int mobile, string voucher)
        {
            var returnData = new ReturnData();
            try
            {

                var taikhoan = Session["TaiKhoan"] != null ? Session["TaiKhoan"].ToString() : string.Empty;

                

                if (taikhoan == null || string.IsNullOrEmpty(taikhoan))
                {
                    returnData.ResponseCode = -11;
                    returnData.Description = "Vui lòng đăng nhập!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                var ma_khach_hang = Session["MaKhacHang"] != null ? Session["MaKhacHang"].ToString() : string.Empty;
                UserLogin session = (UserLogin)Session["USER_SESSION"];
                int makh = Convert.ToInt32(session.taikhoan.MaThanhVien);
                if (ma_khach_hang == null || string.IsNullOrEmpty(ma_khach_hang))
                {
                    returnData.ResponseCode = -121;
                    returnData.Description = "Vui lòng đăng nhập!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


                var list = LayGioHang();
                if (list == null || list.Count <= 0)
                {
                    returnData.ResponseCode = -1; 
                    returnData.Description = "Thông tin đơn hàng không hợp lệ!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }

                var donhang = new tbl_Donhang();
                var sanpham = new tbl_SanPham();
                ////var khachhang = new tbl_KhachHang();
                ////khachhang.makhachhang = 1;
                ////khachhang.tenkhachhang = fullname;
                ////donhang.tbl_KhachHang = khachhang;

                double totalAmount = 0;

                if (!string.IsNullOrEmpty(voucher))
                {
                    if (voucher == "GIAM5")
                    {
                        var voucherValue = list.Sum(s => s.ThanhTien) * 5 / 100;

                        totalAmount = list.Sum(s => s.ThanhTien) - voucherValue;
                    }

                }
                else
                {
                    totalAmount = list.Sum(s => s.ThanhTien);

                }
                var makhachang = db.tbl_KhachHang.Where(x => x.MaThanhVien == makh).ToList();
                donhang.makhachhang = makhachang.First().makhachhang;
                donhang.tenkhachhang = fullname;
                donhang.ngaydathang = DateTime.Now;
                donhang.trangthai = "ATM";// cá
                donhang.tongtien = totalAmount;
                donhang.diachi = address;
                donhang.sdt = mobile;
                donhang.magiamgia = voucher;
                donhang.trangthaidathang = "Tạo mới";
                donhang.IsDaxoa = 0;

                // insert vào db 
               
                db.tbl_Donhang.Add(donhang);
                var result = db.SaveChanges();
                var ma_don_hang = donhang.madonhang;
                madonhang = donhang.madonhang;
                if (result >= 0)
                {
                    // insert xong đơn hàng thì insert vào chi tiết đơn hàng 

                    foreach (var item in list) // list có nhiều sản phẩm thì insert nhiều sản phẩm
                    {
                        var chitietdon = new tbl_ChiTietDonHang();

                        chitietdon.madonhang = ma_don_hang;
                        chitietdon.masp = item.masp;
                        chitietdon.soluong = item.soluong;
                        chitietdon.tongtien = item.ThanhTien;

                        db.tbl_ChiTietDonHang.Add(chitietdon);
                        db.SaveChanges();
                        //  var result_chitiet = db.SaveChanges();
                    }

                    // khi lưu database thành công thì lưu thông tin mã đơn hàng , số tiền và số điện thoại người mua để đẩy sang trang cổng thanh toán Bank
                    // Lưu vào trường Extended ở dạng mã hóa  Base64 

                    var bas64str = Base64Encode(ma_don_hang + "|" + totalAmount + "|" + mobile);

                    //SET LẠI GIỎ HÀNG BẰNG RỖNG 
                   // Session["GioHang"] = null;

                    returnData.ResponseCode = 1;
                    returnData.Description = "Đăt hàng thành công!";
                    returnData.Extended = bas64str;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Đăt hàng thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(returnData);
        }

        public ActionResult ResultPayment(string data)
        {
            //var dao = new THONGTINTHANHTOANDAO();
            //var model = dao.infoSP(id);
            //return View(model);

            var dao = new SANPHAMDAO();

            var model = dao.donHang(madonhang);
            try
            {
                //Ở TRANG KẾT QUẢ THANH TOÁN NÀY SAU KHI NHẬN ĐƯỢC THÔNG TIN TỪ CÔNG THANH TOÁN SẼ XỬ LÝ GIẢI MÃ CHUỔI BASE 64 RA ĐỂ ĐỌC DỮ LIỆU

                
               var dataPlanText = string.Empty;
                var ResponseCode = string.Empty;
                var PartnerTrans = string.Empty;
                var Description = string.Empty;
                if (!string.IsNullOrEmpty(data))
                {
                    dataPlanText = Base64Decode(data);// thực hiện giải mã dữ liệu mà bên cổng thanh toán giửi về 
                    ResponseCode = dataPlanText.Split('|')[0];
                    PartnerTrans = dataPlanText.Split('|')[1];
                    Description = dataPlanText.Split('|')[2];
                }
                //XONG RỒI ĐÓ

                ViewBag.ResponseCode = ResponseCode;
                ViewBag.PartnerTrans = PartnerTrans;
                ViewBag.Description = Description;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(model);
        }

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

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}