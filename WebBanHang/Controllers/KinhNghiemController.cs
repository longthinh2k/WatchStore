using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class KinhNghiemController : Controller
    {
       
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: KinhNghiem
        public ActionResult Index()
        {
            var model = (db.tbl_KinhNghiem).ToList();
            return View(model);
        }
    }
}