using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Areas.Admin.ViewModel;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class BinhLuanController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/BinhLuan
        public ActionResult Index()
        {
            List<ViewBinhLuanSP> list = new List<ViewBinhLuanSP>();
            var data = db.tbl_SanPham.ToList();
            foreach(var item in data)
            {
                ViewBinhLuanSP view = new ViewBinhLuanSP();
                var binhluan = db.tbl_BinhLuan.Where(x => x.masp == item.masp).ToList();
                if (binhluan.Count() > 0)
                {
                    view.Masp = item.masp;
                    view.tenSp = item.tensp;
                    view.soLuong = binhluan.Count();
                    list.Add(view);
                }
            }
            ViewBag.listSanPham = list;
            return View(db.tbl_BinhLuan.ToList());
        }

        [HttpGet]
        public ActionResult ChiTietBinhLuan(int id)
        {
            List<ViewChiTietBinhLuan> list = new List<ViewChiTietBinhLuan>();
            var data = db.tbl_BinhLuan.Where(x => x.masp == id).ToList();
            foreach(var item in data)
            {
                ViewChiTietBinhLuan ct = new ViewChiTietBinhLuan();
                var khachhang = db.tbl_KhachHang.Find(item.makhachhang);
                ct.idBinhLuan = item.mabinhluan;
                ct.tenKhachHang = khachhang.tenkhachhang;
                ct.noiDung = item.noidung;
                ct.idSanPham = item.masp;
                ct.idKhachHang = item.makhachhang;
                ct.ngayBinhLuan = item.ngaybinhluan;
                list.Add(ct);
            }
            ViewBag.listBinhLuan = list;
            ViewBag.BinhLuanId = id;
            return View();
        }
        // GET: Admin/Hangsanxuats/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id, int binhluanId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           tbl_BinhLuan binhluan = db.tbl_BinhLuan.Find(id);
            if (binhluan != null )
            {
                db.tbl_BinhLuan.Remove(binhluan);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("ChiTietBinhLuan", new { id = binhluanId});
            }
            //return RedirectToAction("Index");
            return RedirectToAction("ChiTietBinhLuan", new { id = binhluanId });
        }
        // POST: Admin/Hangsanxuats/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //   tbl_BinhLuan binhluan = db.tbl_BinhLuan.Find(id);
        //    db.tbl_BinhLuan.Remove(binhluan);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}