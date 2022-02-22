using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class LienHeController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: LienHe
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PhanHoi(string HoTen,int SDT,string Email, string TinNhan)
        {
            var LienHe = new tbl_LienHe();
            LienHe.tenlienhe = HoTen;
            LienHe.sodienthoai = SDT;
            LienHe.emaillienhe = Email;
            LienHe.noidung = TinNhan;
            db.tbl_LienHe.Add(LienHe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}