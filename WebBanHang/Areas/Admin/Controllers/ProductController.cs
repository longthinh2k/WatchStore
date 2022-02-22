using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class ProductController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/Product
        [HttpGet]
        public ActionResult Index()
        {
            var lstProduct = db.tbl_SanPham.ToList();
   
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.madanhmuc = new SelectList(db.tbl_DanhMucSanPham.OrderBy(n => n.tendanhmuc), "madanhmuc", "tendanhmuc");
            return View();
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Create(tbl_SanPham loadsp)
        {
            ViewBag.madanhmuc = new SelectList(db.tbl_DanhMucSanPham.OrderBy(n => n.tendanhmuc), "madanhmuc", "tendanhmuc");
           
            db.tbl_SanPham.Add(loadsp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            var chitiet = db.tbl_SanPham.SingleOrDefault(n => n.masp == id);
            return View(chitiet);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var xoasp = db.tbl_SanPham.SingleOrDefault(n => n.masp == id);
            return View(xoasp);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var xoasp = db.tbl_SanPham.SingleOrDefault(n => n.masp == id);
            db.tbl_SanPham.Remove(xoasp);
            db.SaveChanges();
            return RedirectToAction("Index","Product");
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.madanhmuc = new SelectList(db.tbl_DanhMucSanPham.OrderBy(n => n.tendanhmuc), "madanhmuc", "tendanhmuc");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_SanPham sanpham = db.tbl_SanPham.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tensp, giasp, anhsp1, anhsp2, anhsp3, anhsp4, danhgiasanpham, motasanpham1, motasanpham2, motasanpham3, motasanpham4, luotxemsanpham ")] tbl_SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }
    }
    
}