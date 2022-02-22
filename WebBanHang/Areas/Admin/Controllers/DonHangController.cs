using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using System.Web.Security;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class DonHangController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/DonHang
       
        public ActionResult DonHangATM()
        {
            var dh = db.tbl_Donhang.Where(n => n.IsDaxoa == 0 && n.trangthaidathang=="Đã thanh toán" && n.trangthai=="ATM").ToList();
            return View(dh);
        }

        public ActionResult DonHangCOD()
        {
            var dh = db.tbl_Donhang.Where(n => n.IsDaxoa == 0 && n.trangthaidathang == "Đã thanh toán" && n.trangthai =="COD").ToList();
            return View(dh);
        }

        public ActionResult DonHangCODChoDuyet()
        {
            var dh = db.tbl_Donhang.Where(n => n.IsDaxoa == 0 && n.trangthaidathang == "Tạo mới"  && n.trangthai=="COD").ToList();
            return View(dh);
        }


        public ActionResult Details(int? id)
        {

            ViewBag.makhachhang1 = new SelectList(db.tbl_KhachHang.OrderBy(n => n.makhachhang), "makhachhang", "tenkhachhang");
            ViewBag.makhachhang = new SelectList(db.tbl_ThanhVien.OrderBy(n => n.MaThanhVien), "MaThanhVien", "HoTen");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Donhang dh = db.tbl_Donhang.Find(id);
            if (dh == null)
            {
                return HttpNotFound();
            }
            return View(dh);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.makhachhang1 = new SelectList(db.tbl_KhachHang.OrderBy(n => n.makhachhang), "makhachhang", "tenkhachhang");
            ViewBag.makhachhang = new SelectList(db.tbl_ThanhVien.OrderBy(n => n.MaThanhVien), "MaThanhVien", "HoTen");
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Donhang dh = db.tbl_Donhang.Find(id);
            if (dh == null)
            {
                return HttpNotFound();
            }
            return View(dh);
        }

        [HttpGet]

        public ActionResult DuyetDonHang(int? id)
        {

            var duyet = db.tbl_Donhang.SingleOrDefault(n => n.madonhang == id);
            return View(duyet);
        }
        [HttpPost, ActionName("DuyetDonHang")]
        [ValidateAntiForgeryToken]

        public ActionResult DuyetDonHangConfirm(int id)
        {
            var duyet = db.tbl_Donhang.Find(id);
            duyet.trangthaidathang = "Đã thanh toán";
            db.SaveChanges();
            return RedirectToAction("DonHangATM", "DonHang");
        }
        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "makhachhang, ngaydathang, trangthai, tongtien, diachi, sdt, magiamgia, tenkhachhang, trangthaidathang ")] tbl_Donhang dh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dh);
        }





        // GET: Admin/Hangsanxuats/Delete/5
  
        public ActionResult Delete(int? id)
        {
            ViewBag.makhachhang1 = new SelectList(db.tbl_KhachHang.OrderBy(n => n.makhachhang), "makhachhang", "tenkhachhang");
            ViewBag.makhachhang = new SelectList(db.tbl_ThanhVien.OrderBy(n => n.MaThanhVien), "MaThanhVien", "HoTen");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           tbl_Donhang dh = db.tbl_Donhang.Find(id);
            if (dh == null)
            {
                return HttpNotFound();
            }
            return View(dh);
        }

        // POST: Admin/Hangsanxuats/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Donhang dh = db.tbl_Donhang.Find(id);
            dh.IsDaxoa = 1;
            db.SaveChanges();
            return RedirectToAction("DonHangATM");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}