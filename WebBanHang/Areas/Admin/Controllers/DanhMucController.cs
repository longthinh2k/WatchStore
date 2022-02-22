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
    public class DanhMucController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/DanhMuc

        public ActionResult Index()
        {
            return View(db.tbl_DanhMucSanPham.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_DanhMucSanPham danhmucsanpham = db.tbl_DanhMucSanPham.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }

        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madanhmuc,tendanhmuc")] tbl_DanhMucSanPham danhmucsanpham)
        {
            if (ModelState.IsValid)
            {
                db.tbl_DanhMucSanPham.Add(danhmucsanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhmucsanpham);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           tbl_DanhMucSanPham danhmucsanpham = db.tbl_DanhMucSanPham.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madanhmuc,tendanhmuc")] tbl_DanhMucSanPham danhmucsanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhmucsanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhmucsanpham);
        }





        // GET: Admin/Hangsanxuats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_DanhMucSanPham danhmucsanpham = db.tbl_DanhMucSanPham.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }
        // POST: Admin/Hangsanxuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_DanhMucSanPham danhmucsanpham = db.tbl_DanhMucSanPham.Find(id);
            db.tbl_DanhMucSanPham.Remove(danhmucsanpham);
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

    
