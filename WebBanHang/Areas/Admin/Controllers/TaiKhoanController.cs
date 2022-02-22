using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class TaiKhoanController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            return View(db.tbl_ThanhVien.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ThanhVien tv = db.tbl_ThanhVien.Find(id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }

        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThanhVien, TaiKhoan, MatKhau, HoTen, DiaChi, SoDienThoai, Email")] tbl_ThanhVien tv)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ThanhVien.Add(tv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tv);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           tbl_ThanhVien tv = db.tbl_ThanhVien.Find(id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThanhVien, TaiKhoan, MatKhau, HoTen, DiaChi, SoDienThoai, Email")] tbl_ThanhVien tv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tv);
        }

        // GET: Admin/Hangsanxuats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ThanhVien tv = db.tbl_ThanhVien.Find(id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }

        // POST: Admin/Hangsanxuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_ThanhVien tv = db.tbl_ThanhVien.Find(id);
            db.tbl_ThanhVien.Remove(tv);
            db.SaveChanges();
            return RedirectToAction("Index");
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